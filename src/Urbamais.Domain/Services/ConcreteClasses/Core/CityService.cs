using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Core;

namespace Urbamais.Domain.Services.ConcreteClasses.Core;

public class CityService : ServiceBase<City>, ICityService
{
    public CityService(ICityRepository repository) : base(repository)
    {
    }
}