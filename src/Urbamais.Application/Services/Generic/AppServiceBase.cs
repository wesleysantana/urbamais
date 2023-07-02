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

    public IQueryable<T> Query => _serviceBase.Query;

    public Task<int> Commit() => _serviceBase.Commit();

    public void Delete(object id) => _serviceBase.Delete(id);

    public void Dispose() => _serviceBase.Dispose();

    public Task<T> Get(object id) => _serviceBase.Get(id);

    public Task<T> Get(Expression<Func<T, bool>> where) => _serviceBase.Get(where);

    public Task Insert(T entity) => _serviceBase.Insert(entity);

    public Task<IList<T>> List() => _serviceBase.List();

    public Task<IList<T>> List(Expression<Func<T, bool>> where) => _serviceBase.List(where);

    public void Update(T entity) => _serviceBase.Update(entity);
}