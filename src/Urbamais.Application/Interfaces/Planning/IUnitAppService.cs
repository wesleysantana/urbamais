using Urbamais.Application.Interfaces.Generic;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.Interfaces.Planejamento;

public interface IUnitAppService : IAppServiceBase<Unidade>
{
    Task<List<Insumo>> GetInputs(int unitId);
}