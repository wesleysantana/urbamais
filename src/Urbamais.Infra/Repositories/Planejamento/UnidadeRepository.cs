using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Planejamento;

public class UnidadeRepository : RepositoryBase<Unidade>, IUnidadeRepository
{
    public UnidadeRepository(ContextEf context) : base(context)
    {
    }
}