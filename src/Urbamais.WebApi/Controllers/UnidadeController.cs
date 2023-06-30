using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Urbamais.Application.App.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request.Unidade;
using Urbamais.Application.ViewModels.Response.Unidade;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnidadeController : ControllerBase
{
    private readonly IUnidadeApp _unidadeApp;
    private readonly IMapper _mapper;

    public UnidadeController(IUnidadeApp unidadeApp, IMapper mapper)
    {
        _unidadeApp = unidadeApp;
        _mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    public async Task<ActionResult<List<UnidadeResponse>>> Get()
    {
        return Ok(_mapper.Map<List<UnidadeResponse>>(await _unidadeApp.List()));
    }

    [HttpPost]
    //[Authorize]
    public async Task<ActionResult<UnidadeResponse>> Insert(UnidadeRequest unidadeRequest)
    {
        var unidadeResponse = await _unidadeApp.Insert(_mapper.Map<Unidade>(unidadeRequest));

        if (unidadeResponse.IsValid)
        {
            return Ok(unidadeResponse);
        }

        return BadRequest(unidadeResponse);
    }
}