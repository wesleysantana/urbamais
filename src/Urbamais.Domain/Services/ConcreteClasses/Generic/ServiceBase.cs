using System.Linq.Expressions;
using Urbamais.Domain.InterfacesRepositories.Generic;
using Urbamais.Domain.Services.Interfaces.Generic;

namespace Urbamais.Domain.Services.ConcreteClasses.Generic;

public class ServiceBase<T> : IDisposable, IServiceBase<T> where T : class
{
    private readonly IRepositoryBase<T> _repository;

    public ServiceBase(IRepositoryBase<T> repository)
    {
        _repository = repository;
    }

    public Task<int> Commit() => _repository.Commit();

    public Task Insert(T entity) => _repository.Insert(entity);

    public void Update(T entity) => _repository.Update(entity);

    public void Delete(object id) => _repository.Delete(id);

    #region Query

    public IQueryable<T> Query => _repository.Query();

    public Task<T> Get(object id) => _repository.Get(id);

    public Task<T> Get(Expression<Func<T, bool>> where, CancellationToken cancellationToken) =>
        _repository.Get(where, cancellationToken);

    public Task<IList<T>> List(CancellationToken cancellationToken) => _repository.List(cancellationToken);

    public Task<IList<T>> List(Expression<Func<T, bool>> where, CancellationToken cancellationToken) =>
        _repository.List(where, cancellationToken);

    #endregion Query

    public void Dispose() => _repository.Dispose();
}