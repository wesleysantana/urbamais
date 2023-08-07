using Urbamais.Application.Interfaces.Planning;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.Services.Interfaces.Planejamentos;

namespace Urbamais.Application.Services.Planejamentos;

public class InsumoAppService : AppServiceBase<Insumo>, IInsumoAppService
{
    public InsumoAppService(IInsumoService serviceBase) : base(serviceBase)
    {
    }
}