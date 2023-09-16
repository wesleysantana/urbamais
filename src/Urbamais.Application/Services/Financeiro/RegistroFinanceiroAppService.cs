using Urbamais.Application.Interfaces.Financeiro;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Financeiro;
using Urbamais.Domain.Services.Interfaces.Financeiro;

namespace Urbamais.Application.Services.Financeiro;

public class RegistroFinanceiroAppService : AppServiceBase<RegistroFinanceiro>, IRegistroFinanceiroAppService
{
    public RegistroFinanceiroAppService(IRegistroFinanceiroService serviceBase) : base(serviceBase)
    {
    }
}