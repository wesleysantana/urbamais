using Urbamais.Domain.Entities.CoreRelationManyToMany;
using Urbamais.Domain.Repositories.Interfaces.Core;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Core;

public class CidadeRepository : RepositoryBase<Cidade>, ICidadeRepository
{
    public CidadeRepository(ContextEf context) : base(context)
    {
    }
}