using Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Urbamais.Domain.InterfacesRepositories.Generic;
using Urbamais.Infra.Config;

namespace Urbamais.Infra.Repositories.Generic;

public class RepositoryBase<T> : IDisposable, IUnitOfWork, IRepositoryBase<T> where T : class
{
    protected readonly ContextEf _context;

    public RepositoryBase(ContextEf context)
    {
        _context = context;
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

    #region Querys
    public IQueryable<T> Query => _context.Set<T>().AsQueryable<T>();

    public async Task<T> Get(object id) => (await _context.Set<T>().FindAsync(id))!;

    public async Task<T> Get(Expression<Func<T, bool>> where) => (await _context.Set<T>().Where(where).FirstOrDefaultAsync())!;

    public async Task<IList<T>> List()
    {
        var entidade = typeof(T);
        if (entidade?.GetType() == typeof(BaseEntity))
            return (await _context.Set<BaseEntity>().Where(x => x.DataExclusao == null).AsNoTracking().ToListAsync() as List<T>)!;

        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<IList<T>> List(Expression<Func<T, bool>> where) => await _context.Set<T>().Where(where).AsNoTracking().ToListAsync();
    #endregion

    public void Dispose() => _context.DisposeAsync();
}