using System.Linq.Expressions;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Application.App.Interfaces.Generic;

public interface IApp<T> where T : class
{
    Task<T> Insert(T entity);

    Task<Tuple<bool, T>> Update(object id, IDomainUpdate entity);

    Task<Tuple<bool, bool>> Delete(object id, string IdUserDeletion);

    Task<int> Commit();

    #region Querys

    //Task<IQueryable<T>> Query(IFiltroRequest query);
    Task<IList<Unit>> Query(IFilterRequest filter, CancellationToken cancellationToken);

    Task<T> Get(object id);

    Task<T> Get(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    Task<IList<T>> List(CancellationToken cancellationToken);

    Task<IList<T>> List(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    Task<IList<T>> ResultQuery(IQueryable<T> query, CancellationToken cancellationToken);

    #endregion Querys
}