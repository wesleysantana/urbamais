using AutoMapper;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Generic;
using Urbamais.Application.ViewModels.Response.Unidade;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Domain.Services.Interfaces.Planejamento;

namespace Urbamais.Application.Services.Planejamento;

public class UnidadeAppService : AppServiceBase<Unidade>, IUnidadeAppService
{
    private readonly IMapper _mapper;
    private readonly IUnidadeService _unidadeService;

    public UnidadeAppService(IUnidadeService service, IMapper mapper) : base(service)
    {
        _unidadeService = service;
        _mapper = mapper;
    }

    public async Task<List<UnidadeResponse>> Get() =>
            _mapper.Map<List<UnidadeResponse>>(await _unidadeService.List());

    public async Task Create(Unidade unidade) =>
        await _unidadeService.Insert(unidade);
}