using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.Services.Interfaces.Planejamentos;

namespace Urbamais.Application.Services.Planejamentos;

public class UnidadeAppService : AppServiceBase<Unidade>, IUnidadeAppService
{
    private readonly IUnidadeService _unitService;

    public UnidadeAppService(IUnidadeService service) : base(service)
    {
        _unitService = service;
    }

    public async Task<List<Insumo>> GetInsumos(int unidadeId) => await _unitService.GetInsumos(unidadeId);
}