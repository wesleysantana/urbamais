using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Services.Interfaces.Generic;
using Urbamais.Application.Interfaces.Core;

namespace Urbamais.Application.Services.Core;

public class CityAppService : AppServiceBase<Cidade>, ICityAppService
{
    public CityAppService(IServiceBase<Cidade> serviceBase) : base(serviceBase)
    {
    }
}