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

    public void Update(T entity) => _context.Update(entity);

    public void Delete(object id)
    {
        var entity = Get(id).Result;
        if (entity is null)
            return;

        if (entity.GetType().BaseType == typeof(BaseEntity))
        {
            var entidade = entity as BaseEntity;
            entidade?.Delete();
            return;
        }

        _context.Set<T>().Remove(entity);
    }

    public Task<int> Commit()
    {
        try
        {
            var result = _context.SaveChangesAsync();
            if (result.Result == 0)
                throw new Exception();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar gravar os dados: " + ex.Message);
        }
    }

    public Task Rollback() => Task.CompletedTask;

    #region Querys

    public IQueryable<T> Query() => _context.Set<T>().AsQueryable();

    public virtual async Task<T> Get(object id)
    {
        try
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }

    public virtual async Task<T> Get(Expression<Func<T, bool>> where)
    {
        try
        {
            return (await _context.Set<T>().Where(where).FirstOrDefaultAsync())!;
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }

    public virtual async Task<IList<T>> List()
    {
        try
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }

    public virtual async Task<IList<T>> List(Expression<Func<T, bool>> where)
    {
        try
        {
            return await _context.Set<T>().Where(where).AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }

    #endregion Querys

    public void Dispose() => _context?.DisposeAsync();
}