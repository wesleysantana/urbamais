using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Planejamento;

namespace Urbamais.Domain.Services.ConcreteClasses.Planejamento;

public class UnitService : ServiceBase<Unit>, IUnitService
{
    public UnitService(IUnitRepository repository) : base(repository)
    {
    }
}