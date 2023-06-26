using System.Linq.Expressions;

namespace Urbamais.Domain.Services.Interfaces.Generic;

public interface IServiceBase<T> where T : class
{
    Task Insert(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<int> Commit();

    #region Querys

    IQueryable<T> Query { get; }

    Task<T> Get(object id);

    Task<T> Get(Expression<Func<T, bool>> @where);

    Task<IList<T>> List();

    Task<IList<T>> List(Expression<Func<T, bool>> @where);

    #endregion Querys

    void Dispose();
}