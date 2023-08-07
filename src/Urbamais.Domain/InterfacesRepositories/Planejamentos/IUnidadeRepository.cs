using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Generic;

namespace Urbamais.Domain.InterfacesRepositories.Planejamentos;

public interface IUnidadeRepository : IRepositoryBase<Unidade>
{
    Task<List<Insumo>> GetInputs(int unitId);
}