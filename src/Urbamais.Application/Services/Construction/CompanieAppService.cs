using Urbamais.Application.Interfaces.Construction;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.Services.Interfaces.Construction;

namespace Urbamais.Application.Services.Construction;

public class CompanieAppService : AppServiceBase<Empresa>, ICompanieAppService
{
    public CompanieAppService(ICompanieService serviceBase) : base(serviceBase)
    {
    }
}