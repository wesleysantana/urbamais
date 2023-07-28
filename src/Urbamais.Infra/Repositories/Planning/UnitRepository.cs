using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Planning;

public class UnitRepository : RepositoryBaseEntity<Unit>, IUnitRepository
{
    public UnitRepository(ContextEf context) : base(context)
    {
    }

    public async Task<List<Input>> GetInputs(int unitId) =>
         await _context.Inputs.Where(x => x.DeletionDate == null && x.UnitId == unitId)
            .AsNoTracking()
            .ToListAsync();
}