//using System.Linq.Expressions;
//using System.Threading;
//using Urbamais.Domain.InterfacesRepositories.Generic;
//using Urbamais.Domain.Services.Interfaces.Generic;

//namespace Urbamais.Domain.Services.ConcreteClasses.Generic;

//public class ServiceBase<T> : IDisposable, IServiceBase<T> where T : class
//{
//    private readonly IRepositoryBase<T> _repository;

//    public ServiceBase(IRepositoryBase<T> repository)
//    {
//        _repository = repository;
//    }

//    public Task<int> SaveChangesAsync(CancellationToken ct) => _repository.SaveChangesAsync(ct);

//    public Task Insert(T entity) => _repository.Insert(entity);

//    public void Update(T entity) => _repository.Update(entity);

//    public void Delete(object id) => _repository.Delete(id);

//    #region Query

//    public IQueryable<T> Query => _repository.Query();

//    public async Task<T> Get(object id) => await _repository.Get(id);

//    public async Task<T> Get(Expression<Func<T, bool>> where, CancellationToken cancellationToken) =>
//        await _repository.Get(where, cancellationToken);

//    public async Task<IList<T>> List(CancellationToken cancellationToken) => await _repository.List(cancellationToken);

//    public async Task<IList<T>> List(Expression<Func<T, bool>> where, CancellationToken cancellationToken) =>
//        await _repository.List(where, cancellationToken);

//    public async Task<IList<T>> ResultQuery(IQueryable<T> query, CancellationToken cancellationToken) =>
//        await _repository.ResultQuery(query, cancellationToken);

//    #endregion Query

//    public void Dispose() => _repository.Dispose();

//    public Task<T> Get(Expression<Func<T, bool>> where)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IList<T>> List()
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IList<T>> List(Expression<Func<T, bool>> where)
//    {
//        throw new NotImplementedException();
//    }
//}