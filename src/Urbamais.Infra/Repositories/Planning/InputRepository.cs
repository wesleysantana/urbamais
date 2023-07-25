using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Planning;

public class InputRepository : RepositoryBaseEntity<Input>, IInputRepository
{
    public InputRepository(ContextEf context) : base(context)
    {
    }
}