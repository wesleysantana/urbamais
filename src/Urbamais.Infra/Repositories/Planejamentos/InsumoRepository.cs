using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Planejamentos;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Planning;

public class InsumoRepository : RepositoryBaseEntity<Insumo>, IInsumoRepository
{
    public InsumoRepository(ContextEf context) : base(context)
    {        
    }    

    public override async Task<IList<Insumo>> ResultQuery(IQueryable<Insumo> query, CancellationToken cancellationToken)
    {
        try
        {
            return await query.Where(x => x.DeletionDate == null)
                .Include(x => x.Unidade)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }

    public override Task<Insumo> Get(object id)
    {
        try
        {
            return Task.FromResult(_context.Insumos
                .Include(x => x.Unidade)
                .FirstOrDefault(x => x.DeletionDate == null && x.Id == (int)id)!);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }
}