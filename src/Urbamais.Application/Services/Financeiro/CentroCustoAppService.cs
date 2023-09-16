using Urbamais.Application.Interfaces.Financeiro;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Financeiro;
using Urbamais.Domain.Services.Interfaces.Financeiro;

namespace Urbamais.Application.Services.Financeiro;

public class CentroCustoAppService : AppServiceBase<CentroCusto>, ICentroCustoAppService
{
    public CentroCustoAppService(ICentroCustoService serviceBase) : base(serviceBase)
    {
    }
}