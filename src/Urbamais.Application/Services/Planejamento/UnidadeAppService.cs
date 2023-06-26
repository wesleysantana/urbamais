using AutoMapper;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Domain.Services.Interfaces.Planejamento;

namespace Urbamais.Application.Services.Planejamento;

public class UnidadeAppService : AppServiceBase<Unidade>, IUnidadeAppService
{
    public UnidadeAppService(IUnidadeService service) : base(service)
    {
    }
}