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

    public IQueryable<T> Query => _repository.Query;

    public void Delete(T entity) => _repository.Delete(entity);

    public Task<T> Get(object id) => _repository.Get(id);

    public Task<T> Get(Expression<Func<T, bool>> where) => _repository.Get(where);

    public Task Insert(T entity) => _repository.Insert(entity);

    public Task<IList<T>> List() => _repository.List();

    public Task<IList<T>> List(Expression<Func<T, bool>> where) => _repository.List(where);

    public void Update(T entity) => _repository.Update(entity);

    public void Dispose() => _repository.Dispose();
}