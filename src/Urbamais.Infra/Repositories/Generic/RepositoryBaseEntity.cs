﻿using Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using Urbamais.Infra.Config;

namespace Urbamais.Infra.Repositories.Generic;

public class RepositoryBaseEntity<T> : RepositoryBase<T> where T : BaseEntity
{
    private new readonly ContextEf _context;

    public RepositoryBaseEntity(ContextEf context) : base(context)
    {
        _context = context;
    }

    public override Task<T> Get(object id)
    {
        try
        {
            return Task.FromResult(_context.Set<T>().FirstOrDefault(x => x.DeletionDate == null && x.Id == (int)id)!);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }

    public override async Task<IList<T>> List(CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Set<T>().Where(x => x.DeletionDate == null).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }

    public override async Task<IList<T>> ResultQuery(IQueryable<T> query, CancellationToken cancellationToken)
    {
        try
        {
            return await query.Where(x => x.DeletionDate == null).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }
}