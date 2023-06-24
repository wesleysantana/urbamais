using Urbamais.Application.ConcreteClasses.Generic;
using Urbamais.Application.Interfaces.Services;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Services.Interfaces.Generic;

namespace Urbamais.Application.ConcreteClasses.Core;

public class CidadeAppService : AppServiceBase<Cidade>, ICidadeAppService
{
    public CidadeAppService(IServiceBase<Cidade> serviceBase) : base(serviceBase)
    {
    }
}