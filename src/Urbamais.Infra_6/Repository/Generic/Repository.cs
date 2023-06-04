using Core.SeedWork;
using Urbamais.Domain.Interfaces.Generric;
using Urbamais.Infra.Config;

namespace Urbamais.Infra.Repository.Generic;

public abstract class Repository<T> : QueryRepository<T>, IUnitOfWork, IRepository<T> where T : class
{
    public Repository(ContextEf set) : base(set)
    {
    }

    public async Task Insert(T entity) => await _context.Set<T>().AddAsync(entity);

    public void Update(T entity) => _context.Set<T>().Update(entity);

    public void Delete(T entity)
    {
        if (entity.GetType() == typeof(BaseEntity))
        {
            var entidade = entity as BaseEntity;
            entidade?.Delete();
            return;
        }

        _context.Set<T>().Remove(entity);
    }

    public Task<int> Commit() => _context.SaveChangesAsync();

    public Task Rollback() => Task.CompletedTask;
}