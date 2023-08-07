using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.InterfacesRepositories.Obras;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Obras;

namespace Urbamais.Domain.Services.ConcreteClasses.Obras;

public class EmpresaService : ServiceBase<Empresa>, IEmpresaService
{
    public EmpresaService(IEmpresaRepository repository) : base(repository)
    {
    }
}