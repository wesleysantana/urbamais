using Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Urbamais.Domain.Interfaces.Generic;
using Urbamais.Infra.Config;

namespace Urbamais.Infra.Repository.Generic;

public abstract class QueryRepository<T> : IQueryRepository<T> where T : class
{
    protected readonly ContextEf _context;

    public QueryRepository(ContextEf context)
    {
        _context = context;
    }

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
}