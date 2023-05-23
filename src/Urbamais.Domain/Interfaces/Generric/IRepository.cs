using Urbamais.Domain.Interfaces.Generic;

namespace Urbamais.Domain.Interfaces.Generric;

public interface IRepository<T> : IQueryRepository<T> where T : class
{
    Task<int> Insert(T entity);

    Task<int> Update(T entity);

    Task<int> Delete(T entity);
}