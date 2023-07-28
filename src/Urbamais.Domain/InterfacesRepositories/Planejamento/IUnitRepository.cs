using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.InterfacesRepositories.Generic;

namespace Urbamais.Domain.InterfacesRepositories.Planejamento;

public interface IUnitRepository : IRepositoryBase<Unit>
{
    Task<List<Input>> GetInputs(int unitId);
}