using System.Linq.Expressions;

namespace Urbamais.Application.App.Interfaces.Generic;

public interface IApp<T> where T : class
{
    Task<T> Insert(T entity);

    Task<T> Update(T entity);

    void Delete(T entity);   

    Task<int> Commit();

    #region Querys

    IQueryable<T> Query { get; }

    Task<T> Get(object id);

    Task<T> Get(Expression<Func<T, bool>> @where);

    Task<IList<T>> List();

    Task<IList<T>> List(Expression<Func<T, bool>> @where);

    #endregion Querys
}