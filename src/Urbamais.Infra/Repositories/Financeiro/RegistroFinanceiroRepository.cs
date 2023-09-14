using Urbamais.Domain.Entities.Financeiro;
using Urbamais.Domain.InterfacesRepositories.Financeiro;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Financeiro;

public class RegistroFinanceiroRepository : RepositoryBaseEntity<RegistroFinanceiro>, IRegistroFinanceiroRepository
{
    public RegistroFinanceiroRepository(ContextEf context) : base(context)
    {
    }
}