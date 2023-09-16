using Urbamais.Domain.Entities.Financeiro;
using Urbamais.Domain.InterfacesRepositories.Financeiro;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Financeiro;

namespace Urbamais.Domain.Services.ConcreteClasses.Financeiro;

public class CentroCustoService : ServiceBase<CentroCusto>, ICentroCustoService
{
    public CentroCustoService(ICentroCustoRepository repository) : base(repository)
    {
    }
}