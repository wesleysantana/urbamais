using System.Linq.Expressions;
using Urbamais.Application.Interfaces.Generic;
using Urbamais.Domain.Services.Interfaces.Generic;

namespace Urbamais.Application.Services.Generic;

public class AppServiceBase<T> : IDisposable, IAppServiceBase<T> where T : class
{
    private readonly IServiceBase<T> _serviceBase;

    public AppServiceBase(IServiceBase<T> serviceBase)
    {
        _serviceBase = serviceBase;
    }

    public Task Insert(T entity) => _serviceBase.Insert(entity);

    public Task<int> SaveChangesAsync(CancellationToken ct) => _serviceBase.SaveChangesAsync(ct);

    public void Delete(object id) => _serviceBase.Delete(id);

    public void Update(T entity) => _serviceBase.Update(entity);

    #region Query

    public IQueryable<T> Query => _serviceBase.Query;

    public async Task<T> Get(object id) => await _serviceBase.Get(id);

    public async Task<T> Get(Expression<Func<T, bool>> where, CancellationToken cancellationToken) =>
        await _serviceBase.Get(where, cancellationToken);

    public async Task<IList<T>> List(CancellationToken cancellationToken) => await _serviceBase.List(cancellationToken);

    public async Task<IList<T>> List(Expression<Func<T, bool>> where, CancellationToken cancellationToken) =>
        await _serviceBase.List(where, cancellationToken);

    public async Task<IList<T>> ResultQuery(IQueryable<T> query, CancellationToken cancellationToken) =>
        await _serviceBase.ResultQuery(query, cancellationToken);

    #endregion Query

    public void Dispose() => _serviceBase.Dispose();
}