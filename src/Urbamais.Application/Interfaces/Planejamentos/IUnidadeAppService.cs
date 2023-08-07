using Urbamais.Application.Interfaces.Generic;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.Interfaces.Planejamento;

public interface IUnidadeAppService : IAppServiceBase<Unidade>
{
    Task<List<Insumo>> GetInsumos(int unidadeId);
}