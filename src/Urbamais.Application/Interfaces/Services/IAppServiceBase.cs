using System.Linq.Expressions;

namespace Urbamais.Application.Interfaces.Services;

public interface IAppServiceBase<T> where T : class
{
    Task Insert(T entity);

    void Update(T entity);

    void Delete(T entity);

    #region Querys

    IQueryable<T> Query { get; }

    Task<T> Get(object id);

    Task<T> Get(Expression<Func<T, bool>> @where);

    Task<IList<T>> List();

    Task<IList<T>> List(Expression<Func<T, bool>> @where);

    #endregion Querys

    void Dispose();
}