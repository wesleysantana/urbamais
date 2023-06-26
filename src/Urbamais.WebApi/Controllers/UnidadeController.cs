using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Urbamais.Application.App.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request.Unidade;
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
        if (ModelState.IsValid)
        {
            _unidadeApp.Insert(_mapper.Map<Unidade>(unidadeRequest));
        }

        return Ok();
    }
}