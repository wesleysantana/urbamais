using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Core;

namespace Urbamais.Domain.Services.ConcreteClasses.Core;

public class CidadeService : ServiceBase<Cidade>, ICityService
{
    public CidadeService(ICityRepository repository) : base(repository)
    {
    }
}