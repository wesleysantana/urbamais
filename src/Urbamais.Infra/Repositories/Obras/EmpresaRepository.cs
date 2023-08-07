using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.InterfacesRepositories.Obras;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Obras;

public class EmpresaRepository : RepositoryBaseEntity<Empresa>, IEmpresaRepository
{
    public EmpresaRepository(ContextEf context) : base(context)
    {
    }
}