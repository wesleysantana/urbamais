using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Planejamentos;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Planning;

public class UnidadeRepository : RepositoryBaseEntity<Unidade>, IUnidadeRepository
{
    public UnidadeRepository(ContextEf context) : base(context)
    {
    }

    public async Task<List<Insumo>> GetInputs(int unitId) =>
         await _context.Insumos.Where(x => x.DeletionDate == null && x.UnidadeId == unitId)
            .AsNoTracking()
            .ToListAsync();
}