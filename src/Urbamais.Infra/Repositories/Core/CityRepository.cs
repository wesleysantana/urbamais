using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Infra.Config;
using Urbamais.Infra.Repositories.Generic;

namespace Urbamais.Infra.Repositories.Core;

public class CityRepository : RepositoryBaseEntity<Cidade>, ICityRepository
{
    public CityRepository(ContextEf context) : base(context)
    {
    }
}