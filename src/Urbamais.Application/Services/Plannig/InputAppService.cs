using Urbamais.Application.Interfaces.Planning;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.Services.Interfaces.Planning;

namespace Urbamais.Application.Services.Plannig;

public class InputAppService : AppServiceBase<Input>, IInputAppService
{
    public InputAppService(IInputService serviceBase) : base(serviceBase)
    {
    }
}