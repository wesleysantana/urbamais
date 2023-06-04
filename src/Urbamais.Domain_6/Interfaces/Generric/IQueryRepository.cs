using System.Linq.Expressions;

namespace Urbamais.Domain.Interfaces.Generic;

public interface IQueryRepository<T> where T : class
{
    IQueryable<T> Query { get; }
    Task<T> Get(object id);
    Task<T> Get(Expression<Func<T, bool>> @where);
    Task<IList<T>> List();
    Task<IList<T>> List(Expression<Func<T, bool>> @where);
}