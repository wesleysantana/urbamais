using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.Services.Interfaces.Planejamento;

namespace Urbamais.Application.Services.Planning;

public class UnitAppService : AppServiceBase<Unit>, IUnitAppService
{
    public UnitAppService(IUnitService service) : base(service)
    {
    }
}