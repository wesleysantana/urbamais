using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Planning;

public class InputRepository : RepositoryBaseEntity<Input>, IInputRepository
{
    public InputRepository(ContextEf context) : base(context)
    {        
    }    

    public override async Task<IList<Input>> ResultQuery(IQueryable<Input> query, CancellationToken cancellationToken)
    {
        try
        {
            return await query.Where(x => x.DeletionDate == null)
                .Include(x => x.Unit)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }

    public override Task<Input> Get(object id)
    {
        try
        {
            return Task.FromResult(_context.Inputs
                .Include(x => x.Unit)
                .FirstOrDefault(x => x.DeletionDate == null && x.Id == (int)id)!);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao tentar consultar a base de dados: " + ex.Message);
        }
    }
}