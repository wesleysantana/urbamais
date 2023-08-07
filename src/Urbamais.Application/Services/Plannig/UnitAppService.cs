using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.Services.Interfaces.Planejamentos;

namespace Urbamais.Application.Services.Planning;

public class UnitAppService : AppServiceBase<Unidade>, IUnitAppService
{
    private readonly IUnidadeService _unitService;

    public UnitAppService(IUnidadeService service) : base(service)
    {
        _unitService = service;
    }

    public async Task<List<Insumo>> GetInputs(int unitId) => await _unitService.GetInsumos(unitId);
}