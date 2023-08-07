using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.InterfacesRepositories.Construction;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Construction;

public class CompanieRepository : RepositoryBaseEntity<Empresa>, ICompanieRepository
{
    public CompanieRepository(ContextEf context) : base(context)
    {
    }
}