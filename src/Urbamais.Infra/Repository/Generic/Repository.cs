using Core.SeedWork;
using Urbamais.Domain.Interfaces.Generric;
using Urbamais.Infra.Config;

namespace Urbamais.Infra.Repository.Generic;

public abstract class Repository<T> : QueryRepository<T>, IUnitOfWork, IRepository<T> where T : class
{
    public Repository(ContextEf set) : base(set)
    {
    }

    public async Task<int> Insert(T entity)
    {        
        await _context.Set<T>().AddAsync(entity);
        return await Commit();
    }

    public async Task<int> Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return await Commit();
    }

    public async Task<int> Delete(T entity)
    {
        if (entity.GetType() == typeof(BaseEntity))
        {
            var entidade = entity as BaseEntity;
            entidade?.Delete();
            return await Commit();
        }

        _context.Set<T>().Remove(entity);
        return await Commit();
    }

    public async Task<int> Commit() => await _context.SaveChangesAsync();

    public Task Rollback() => Task.CompletedTask;
}