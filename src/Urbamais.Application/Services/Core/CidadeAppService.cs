using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Services.Interfaces.Generic;
using Urbamais.Application.Interfaces.Core;

namespace Urbamais.Application.Services.Core;

public class CidadeAppService : AppServiceBase<Cidade>, ICidadeAppService
{
    public CidadeAppService(IServiceBase<Cidade> serviceBase) : base(serviceBase)
    {
    }
}