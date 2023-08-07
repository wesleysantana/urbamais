using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Planejamentos;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Planejamentos;

namespace Urbamais.Domain.Services.ConcreteClasses.Planejamentos;

public class InsumoService : ServiceBase<Insumo>, IInsumoService
{
    public InsumoService(IInsumoRepository repository) : base(repository)
    {
    }
}