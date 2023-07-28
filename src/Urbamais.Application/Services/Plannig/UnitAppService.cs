using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.Services.Interfaces.Planning;

namespace Urbamais.Application.Services.Planning;

public class UnitAppService : AppServiceBase<Unit>, IUnitAppService
{
    private readonly IUnitService _unitService;

    public UnitAppService(IUnitService service) : base(service)
    {
        _unitService = service;
    }

    public async Task<List<Input>> GetInputs(int unitId) => await _unitService.GetInputs(unitId);
}