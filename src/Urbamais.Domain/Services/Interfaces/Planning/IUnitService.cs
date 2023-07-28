using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.Services.Interfaces.Generic;

namespace Urbamais.Domain.Services.Interfaces.Planning;

public interface IUnitService : IServiceBase<Unit>
{
    Task<List<Input>> GetInputs(int unitId);
}