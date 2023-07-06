using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Urbamais.Application.App.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Application.ViewModels.Response.v1.Unidade;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
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
    public async Task<ActionResult<List<UnidadeResponse>>> Get([FromQuery] UnidadeFiltroRequest filtro)
    {
        try
        {
            var response = await _unidadeApp.Query(filtro);
            if (response is not null && response.Any())
                return Ok(_mapper.Map<List<UnidadeResponse>>(response));

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
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

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
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
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<UnidadeResponse>(unidade));
            }

            var problemDetail =
                new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                errors: unidade.ValidationResult!.Errors.Select(x => x.ErrorMessage));

            return BadRequest(problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
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
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (unidade.Item2.IsValid)
            {
                return Ok(_mapper.Map<UnidadeResponse>(unidade.Item2));
            }

            var problemDetail =
                new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                errors: unidade.Item2.ValidationResult!.Errors.Select(x => x.ErrorMessage));

            return BadRequest(problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
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
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (unidade.Item2)
            {
                return NoContent();
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }
}