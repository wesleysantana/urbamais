using System.Linq.Expressions;

namespace Urbamais.Domain.InterfacesRepositories.Generic;

public interface IRepositoryBase<T> where T : class
{
    Task Insert(T entity);

    void Update(T entity);

    void Delete(object id);

    Task<int> SaveChangesAsync(CancellationToken ct = default);

    void Dispose();

    #region Querys

    IQueryable<T> Query();

    Task<T> GetByIdAsync(int id, CancellationToken ct = default);

    Task<T> Get(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    Task<IList<T>> List(CancellationToken cancellationToken);

    Task<IList<T>> List(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    Task<IList<T>> ResultQuery(IQueryable<T> query, CancellationToken cancellationToken);

    #endregion Querys
}