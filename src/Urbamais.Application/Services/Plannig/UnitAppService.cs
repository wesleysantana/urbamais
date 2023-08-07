using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.Services.Interfaces.Planning;

namespace Urbamais.Application.Services.Planning;

public class UnitAppService : AppServiceBase<Unidade>, IUnitAppService
{
    private readonly IUnitService _unitService;

    public UnitAppService(IUnitService service) : base(service)
    {
        _unitService = service;
    }

    public async Task<List<Insumo>> GetInputs(int unitId) => await _unitService.GetInputs(unitId);
}