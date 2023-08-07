using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Generic;

namespace Urbamais.Domain.InterfacesRepositories.Planejamento;

public interface IUnitRepository : IRepositoryBase<Unidade>
{
    Task<List<Insumo>> GetInputs(int unitId);
}