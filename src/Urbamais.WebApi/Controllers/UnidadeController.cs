using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Urbamais.Application.App.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request.Unidade;
using Urbamais.Application.ViewModels.Response.Unidade;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.WebApi.Shared;

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
    public async Task<ActionResult<List<UnidadeResponse>>> Get()
    {
        try
        {
            return Ok(_mapper.Map<List<UnidadeResponse>>(await _unidadeApp.List()));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnidadeResponse>> Get(int id)
    {
        try
        {
            var unidade = _mapper.Map<UnidadeResponse>(await _unidadeApp.Get(id));
            if (unidade is not null)
                return Ok(unidade);

            return NotFound(Constantes.NOTFOUND);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<UnidadeResponse>> Insert(UnidadeRequest unidadeRequest)
    {
        try
        {
            var unidade = await _unidadeApp.Insert(_mapper.Map<Unidade>(unidadeRequest));
            if (unidade.IsValid)
            {
                return Ok(_mapper.Map<UnidadeResponse>(unidade));
            }

            return BadRequest(unidade.ValidationResult!.Errors.Select(x => x.ErrorMessage));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UnidadeResponse>> Update(int id, UnidadeUpdateRequest unidadeRequest)
    {
        try
        {
            var unidade = await _unidadeApp.Update(id, unidadeRequest);

            if (!unidade.Item1)
            {
                return NotFound(Constantes.NOTFOUND);
            }

            if (unidade.Item2.IsValid)
            {
                return Ok(_mapper.Map<UnidadeResponse>(unidade.Item2));
            }

            return BadRequest(unidade.Item2.ValidationResult!.Errors.Select(x => x.ErrorMessage));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var unidade = await _unidadeApp.Delete(id);

            if (!unidade.Item1)
            {
                return NotFound(Constantes.NOTFOUND);
            }

            if (unidade.Item2)
            {
                return Ok();
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("filtro")]
    public async Task<ActionResult<List<UnidadeResponse>>> Filtro([FromQuery] UnidadeFiltroRequest filtro)
    {
        try
        {
            var response = await _unidadeApp.Query(filtro);
            if (response is not null)
                return Ok(_mapper.Map<List<UnidadeResponse>>(response));

            return NotFound(Constantes.NOTFOUND);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}