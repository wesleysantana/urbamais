using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Planning;

public class UnitRepository : RepositoryBaseEntity<Unidade>, IUnitRepository
{
    public UnitRepository(ContextEf context) : base(context)
    {
    }

    public async Task<List<Insumo>> GetInputs(int unitId) =>
         await _context.Inputs.Where(x => x.DeletionDate == null && x.UnidadeId == unitId)
            .AsNoTracking()
            .ToListAsync();
}