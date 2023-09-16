using Urbamais.Domain.Entities.Financeiro;
using Urbamais.Domain.InterfacesRepositories.Financeiro;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Financeiro;

public class CentroCustoRepository : RepositoryBaseEntity<CentroCusto>, ICentroCustoRepository
{
    public CentroCustoRepository(ContextEf context) : base(context)
    {
    }
}