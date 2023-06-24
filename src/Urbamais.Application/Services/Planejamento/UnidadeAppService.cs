using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Domain.Services.Interfaces.Generic;
using Urbamais.Application.Interfaces.Planejamento;

namespace Urbamais.Application.Services.Planejamento;

public class UnidadeAppService : AppServiceBase<Unidade>, IUnidadeAppService
{
    public UnidadeAppService(IServiceBase<Unidade> serviceBase) : base(serviceBase)
    {
    }
}