using System.Linq.Expressions;
using System.Net;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Response;

namespace Urbamais.Application.App.Interfaces.Generic;

public interface IApp<T> where T : class
{
    Task<T> Insert(T entity);

    Task<Tuple<HttpStatusCode, IValidateViewModel>> Update(object id, IDomainUpdate entity);

    Task<Tuple<HttpStatusCode, IValidateViewModel>> Delete(object id, string IdUserDeletion);

    Task<int> Commit();

    #region Querys
    
    Task<IList<T>> Query(IFilterRequest filter, CancellationToken cancellationToken);

    Task<T> Get(object id);

    Task<T> Get(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    Task<IList<T>> List(CancellationToken cancellationToken);

    Task<IList<T>> List(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    Task<IList<T>> ResultQuery(IQueryable<T> query, CancellationToken cancellationToken);

    #endregion Querys
}