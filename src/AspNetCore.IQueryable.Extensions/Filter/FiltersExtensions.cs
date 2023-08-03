using Core.ValueObjects;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace AspNetCore.IQueryable.Extensions.Filter
{
    public static class FiltersExtensions
    {
        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> result, ICustomQueryable model)
        {
            if (model == null)
            {
                return result;
            }

            var lastExpression = result.FilterExpression(model);
            return lastExpression == null
                ? result
                : result.Where(lastExpression);
        }

        public static Expression<Func<TEntity, bool>> FilterExpression<TEntity>(this IQueryable<TEntity> result, ICustomQueryable model)
        {
            if (model == null)
            {
                return null;
            }

            Expression lastExpression = null;

            var operations = ExpressionFactory.GetOperators<TEntity>(model);
            foreach (var expression in operations.Ordered())
            {
                Expression fieldToFilter = expression.FieldToFilter;
                Expression filterBy = expression.FilterBy;

                // Verificar se a propriedade é do tipo string e se o filtro não é case-sensitive.
                if (fieldToFilter.Type == typeof(string) && !expression.Criteria.CaseSensitive)
                {
                    fieldToFilter = Expression.Call(fieldToFilter,
                        typeof(string).GetMethod("ToUpper", Array.Empty<Type>()));

                    filterBy = Expression.Call(filterBy,
                        typeof(string).GetMethod("ToUpper", Array.Empty<Type>()));
                }

                // Criar a expressão de filtro com as modificações
                var actualExpression = GetExpression<TEntity>(expression, fieldToFilter, filterBy);

                if (expression.Criteria.UseNot)
                {
                    actualExpression = Expression.Not(actualExpression);
                }

                if (lastExpression == null)
                {
                    lastExpression = actualExpression;
                }
                else
                {
                    if (expression.Criteria.UseOr)
                        lastExpression = Expression.Or(lastExpression, actualExpression);
                    else
                        lastExpression = Expression.And(lastExpression, actualExpression);
                }
            }

            return lastExpression != null ? Expression.Lambda<Func<TEntity, bool>>(lastExpression, operations.ParameterExpression) : null;
        }

        private static Expression GetExpression<TEntity>(ExpressionParser expression, Expression fieldToFilter, Expression filterBy)
        {
            switch (expression.Criteria.Operator)
            {
                case WhereOperator.Equals:
                    return Expression.Equal(fieldToFilter, filterBy);

                case WhereOperator.NotEquals:
                    return Expression.NotEqual(fieldToFilter, filterBy);

                case WhereOperator.GreaterThan:
                    return Expression.GreaterThan(fieldToFilter, filterBy);

                case WhereOperator.LessThan:
                    return Expression.LessThan(fieldToFilter, filterBy);

                case WhereOperator.GreaterThanOrEqualTo:
                    return Expression.GreaterThanOrEqual(fieldToFilter, filterBy);

                case WhereOperator.LessThanOrEqualTo:
                    return Expression.LessThanOrEqual(fieldToFilter, filterBy);

                case WhereOperator.Contains:
                    return ContainsExpression<TEntity>(expression, fieldToFilter, filterBy);

                case WhereOperator.GreaterThanOrEqualWhenNullable:
                    return GreaterThanOrEqualWhenNullable(fieldToFilter, filterBy);

                case WhereOperator.LessThanOrEqualWhenNullable:
                    return LessThanOrEqualWhenNullable(fieldToFilter, filterBy);

                case WhereOperator.StartsWith:
                    return Expression.Call(fieldToFilter,
                        typeof(string).GetMethods()
                            .First(m => m.Name == "StartsWith" && m.GetParameters().Length == 1),
                        filterBy);

                default:
                    return Expression.Equal(fieldToFilter, filterBy);
            }
        }

        private static Expression LessThanOrEqualWhenNullable(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, e1.Type);
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
                e1 = Expression.Convert(e1, e2.Type);

            return Expression.LessThanOrEqual(e1, e2);
        }

        private static Expression GreaterThanOrEqualWhenNullable(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, e1.Type);
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
                e1 = Expression.Convert(e1, e2.Type);

            return Expression.GreaterThanOrEqual(e1, e2);
        }

        private static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private static Expression ContainsExpression<TEntity>(ExpressionParser expression, Expression fieldToFilter, Expression filterBy)
        {
            if (fieldToFilter.Type == typeof(string) && !expression.Criteria.CaseSensitive)
            {
                // Para tipos string não case-sensitive, convertemos o filtro para maiúsculas
                filterBy = Expression.Call(filterBy, typeof(string).GetMethod("ToUpper", Array.Empty<Type>()));
            }

            if (typeof(IEnumerable).IsAssignableFrom(fieldToFilter.Type))
            {
                // Obtém o tipo do elemento da coleção
                var elementType = fieldToFilter.Type.GetElementType() ?? fieldToFilter.Type.GetGenericArguments().FirstOrDefault();

                if (elementType == null)
                {
                    // Se não pudermos determinar o tipo do elemento da coleção, lançamos uma exceção.
                    throw new ArgumentException($"Cannot determine the element type of the collection in property '" +
                        $"{expression.Criteria.Property.Name}'");
                }

                // Verifica se o tipo do elemento da coleção possui o método Contains
                var methodToApplyContains = elementType.GetMethods()
                    .FirstOrDefault(m => m.Name == "Contains" && m.GetParameters().Length == 1);

                if (methodToApplyContains != null)
                {
                    // Se encontrarmos o método Contains, criamos a chamada do método no tipo do elemento da coleção.
                    return Expression.Call(fieldToFilter, methodToApplyContains, filterBy);
                }
            }
            else if (expression.Criteria.Operator == WhereOperator.Contains)
            {
                if (fieldToFilter.Type == typeof(string))
                {
                    // Se a propriedade for uma string, convertemos o filtro para maiúsculas
                    filterBy = Expression.Call(filterBy, typeof(string).GetMethod("ToUpper", Array.Empty<Type>()));
                    var nameProperty = Expression.Call(fieldToFilter,
                        typeof(string).GetMethods()
                            .First(m => m.Name == "ToUpper" && m.GetParameters().Length == 0));

                    // Faz a comparação usando o método string.Contains() (caso-insensitive)
                    return Expression.Call(nameProperty,
                        typeof(string).GetMethods()
                            .First(m => m.Name == "Contains" && m.GetParameters().Length == 1),
                        filterBy);
                }
                else if (fieldToFilter.Type.BaseType == typeof(ValueObjectBase))
                {
                    // Supondo que o NameVO tenha uma propriedade chamada "Value" do tipo string.
                    var nameProperty = Expression.Property(fieldToFilter, "Value"); 

                    // Convertemos o filtro para maiúsculas
                    filterBy = Expression.Call(filterBy, typeof(string).GetMethod("ToUpper", Array.Empty<Type>()));
                    var nameValue = Expression.Call(nameProperty,
                        typeof(string).GetMethods()
                            .First(m => m.Name == "ToUpper" && m.GetParameters().Length == 0));

                    // Faz a comparação usando o método string.Contains() (caso-insensitive)
                    return Expression.Call(nameValue,
                        typeof(string).GetMethods()
                            .First(m => m.Name == "Contains" && m.GetParameters().Length == 1),
                        filterBy);
                }
            }

            // Caso a propriedade não seja uma coleção, ou o operador não é Contains, ou o tipo da propriedade não é suportado,
            // aplicamos uma comparação direta para igualdade ou outro tipo de comparação suportada pelo tipo.
            return expression.Criteria.Operator switch
            {
                WhereOperator.Equals => Expression.Equal(fieldToFilter, filterBy),
                WhereOperator.NotEquals => Expression.NotEqual(fieldToFilter, filterBy),
                WhereOperator.GreaterThan => Expression.GreaterThan(fieldToFilter, filterBy),
                WhereOperator.LessThan => Expression.LessThan(fieldToFilter, filterBy),
                WhereOperator.GreaterThanOrEqualTo => Expression.GreaterThanOrEqual(fieldToFilter, filterBy),
                WhereOperator.LessThanOrEqualTo => Expression.LessThanOrEqual(fieldToFilter, filterBy),
                WhereOperator.StartsWith => Expression.Call(fieldToFilter,
                    typeof(string).GetMethods()
                        .First(m => m.Name == "StartsWith" && m.GetParameters().Length == 1),
                    filterBy),
                // Aqui você pode adicionar outros casos para outros operadores suportados, se necessário.
                _ => throw new NotSupportedException($"The operator '{expression.Criteria.Operator}' is not supported on property '{expression.Criteria.Property.Name}'")
            };
        }
    }
}