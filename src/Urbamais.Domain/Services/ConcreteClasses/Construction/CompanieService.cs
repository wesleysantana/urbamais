using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.InterfacesRepositories.Construction;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Construction;

namespace Urbamais.Domain.Services.ConcreteClasses.Construction;

public class CompanieService : ServiceBase<Empresa>, ICompanieService
{
    public CompanieService(ICompanieRepository repository) : base(repository)
    {
    }
}