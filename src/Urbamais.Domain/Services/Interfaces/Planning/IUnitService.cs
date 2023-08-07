using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.Services.Interfaces.Generic;

namespace Urbamais.Domain.Services.Interfaces.Planning;

public interface IUnitService : IServiceBase<Unidade>
{
    Task<List<Insumo>> GetInputs(int unitId);
}