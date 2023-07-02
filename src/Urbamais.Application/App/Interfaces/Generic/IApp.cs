using System.Linq.Expressions;
using Urbamais.Application.ViewModels.Request;

namespace Urbamais.Application.App.Interfaces.Generic;

public interface IApp<T> where T : class
{
    Task<T> Insert(T entity);

    Task<Tuple<bool, T>> Update(object id, IDomainUpdate entity);

    Task<Tuple<bool, bool>> Delete(object id);   

    Task<int> Commit();

    #region Querys

    IQueryable<T> Query { get; }

    Task<T> Get(object id);

    Task<T> Get(Expression<Func<T, bool>> @where);

    Task<IList<T>> List();

    Task<IList<T>> List(Expression<Func<T, bool>> @where);

    #endregion Querys
}