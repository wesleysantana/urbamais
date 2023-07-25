using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Planejamento;

namespace Urbamais.Domain.Services.ConcreteClasses.Planejamento;

public class InputService : ServiceBase<Input>, IInputService
{
    public InputService(IInputRepository repository) : base(repository)
    {
    }
}