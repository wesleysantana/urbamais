using Core.ValueObjects.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AspNetCore.IQueryable.Extensions.Sort
{
    public static class SortingExtensions
    {
        public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> result, string fields)
        {
            if (string.IsNullOrEmpty(fields))
            {
                return result;
            }

            var useThenBy = false;
            foreach (var sortTerm in fields.Fields())
            {
                var property = PrimitiveExtensions.GetProperty<TEntity>(sortTerm.FieldName());

                if (property != null)
                {
                    var command = useThenBy ? "ThenBy" : "OrderBy";
                    command += sortTerm.IsDescending() ? "Descending" : string.Empty;

                    if (property.PropertyType.BaseType == typeof(ValueObjectBase))
                    {
                        result = ApplyNameSorting(result, property, command, sortTerm.IsDescending());
                    }
                    else
                    {
                        result = ApplySorting(result, property, command);
                    }
                }

                useThenBy = true;
            }

            return result;
        }

        private static IQueryable<TEntity> ApplySorting<TEntity>(IQueryable<TEntity> source, PropertyInfo propertyInfo, string command)
        {
            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.Property(parameter, propertyInfo);

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new[] { type, propertyInfo.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        private static IQueryable<TEntity> ApplyNameSorting<TEntity>(IQueryable<TEntity> source, PropertyInfo propertyInfo, string command, bool isDescending)
        {
            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.Property(parameter, propertyInfo);
            var nameValueProperty = typeof(ValueObjectBase).GetProperty("Value");
            var nameValueAccess = Expression.MakeMemberAccess(propertyAccess, nameValueProperty);

            var orderByExpression = Expression.Lambda(nameValueAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new[] { type, nameValueProperty.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));

            if (isDescending)
            {
                resultExpression = Expression.Call(typeof(Queryable), "OrderByDescending", new[] { type, nameValueProperty.PropertyType },
                    source.Expression, Expression.Quote(orderByExpression));
            }

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static IQueryable<TEntity> Sort<TEntity, TModel>(this IQueryable<TEntity> result, TModel fields) where TModel : IQuerySort
        {
            if (fields == null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            return result.Sort(fields.Sort);
        }
    }
}