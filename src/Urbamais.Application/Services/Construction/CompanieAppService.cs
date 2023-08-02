using Urbamais.Application.Interfaces.Construction;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Construction;
using Urbamais.Domain.Services.Interfaces.Construction;

namespace Urbamais.Application.Services.Construction;

public class CompanieAppService : AppServiceBase<Companie>, ICompanieAppService
{
    public CompanieAppService(ICompanieService serviceBase) : base(serviceBase)
    {
    }
}