using System.Linq.Expressions;

namespace Urbamais.Domain.InterfacesRepositories.Generic;

public interface IRepositoryBase<T> where T : class
{
    Task Insert(T entity);

    void Update(T entity);

    void Delete(object id);

    Task<int> Commit();

    void Dispose();

    #region Querys

    IQueryable<T> Query();

    Task<T> Get(object id);

    Task<T> Get(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    Task<IList<T>> List(CancellationToken cancellationToken);

    Task<IList<T>> List(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    #endregion Querys
}