using Urbamais.Domain.Interfaces.Generic;

namespace Urbamais.Domain.Interfaces.Generric;

public interface IRepository<T> : IQueryRepository<T> where T : class
{
    Task Insert(T entity);

    void Update(T entity);

    void Delete(T entity);
}