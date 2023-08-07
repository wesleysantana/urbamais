using Urbamais.Application.Interfaces.Obras;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.Services.Interfaces.Obras;

namespace Urbamais.Application.Services.Construction;

public class EmpresaAppService : AppServiceBase<Empresa>, IEmpresaAppService
{
    public EmpresaAppService(IEmpresaService serviceBase) : base(serviceBase)
    {
    }
}