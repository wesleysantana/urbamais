using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request.Unidade;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnidadeController : ControllerBase
{
    private readonly IUnidadeAppService _unidadeApp;
    private readonly IMapper _mapper;

    public UnidadeController(IUnidadeAppService unidadeAppService, IMapper mapper)
    {
        _unidadeApp = unidadeAppService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var response = await _unidadeApp.List();
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Insert(UnidadeRequest unidadeRequest)
    {
        _ = _unidadeApp.Insert(_mapper.Map<Unidade>(unidadeRequest));

        return Ok();
    }
}