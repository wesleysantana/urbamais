using Urbamais.Application.App.Interfaces.Generic;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.App.Interfaces.Planning;

public interface IUnitApp : IApp<Unidade>
{
    Task<List<Insumo>> GetInputs(int unitId);
}