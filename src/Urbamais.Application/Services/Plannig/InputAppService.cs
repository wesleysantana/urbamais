using Urbamais.Application.Interfaces.Planning;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.Services.Interfaces.Planejamentos;

namespace Urbamais.Application.Services.Plannig;

public class InputAppService : AppServiceBase<Insumo>, IInputAppService
{
    public InputAppService(IInsumoService serviceBase) : base(serviceBase)
    {
    }
}