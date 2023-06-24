using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Repositories.Interfaces.Core;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Core;

namespace Urbamais.Domain.Services.ConcreteClasses.Core;

public class CidadeService : ServiceBase<Cidade>, ICidadeService
{
    public CidadeService(ICidadeRepository repository) : base(repository)
    {
    }
}