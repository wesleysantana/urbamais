using Urbamais.Application.App.Interfaces.Generic;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.App.Interfaces.Planejamentos;

public interface IUnidadeApp : IApp<Unidade>
{
    Task<List<Insumo>> GetInsumos(int unitId);
}