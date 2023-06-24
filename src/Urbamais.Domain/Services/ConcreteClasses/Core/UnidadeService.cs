using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Core;

namespace Urbamais.Domain.Services.ConcreteClasses.Core;

public class UnidadeService : ServiceBase<Unidade>, IUnidadeService
{
    public UnidadeService(IUnidadeRepository repository) : base(repository)
    {
    }
}