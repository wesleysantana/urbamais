using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Planning;

namespace Urbamais.Domain.Services.ConcreteClasses.Planejamento;

public class InputService : ServiceBase<Insumo>, IInputService
{
    public InputService(IInputRepository repository) : base(repository)
    {
    }
}