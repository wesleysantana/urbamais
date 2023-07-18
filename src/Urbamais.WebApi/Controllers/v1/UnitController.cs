using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.App.Interfaces.Planning;
using Urbamais.Application.ViewModels.Request.v1.Unit;
using Urbamais.Application.ViewModels.Response.v1.Unit;
using Urbamais.Domain.Entities.Planning;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UnitController : ControllerBase
{
    private readonly IUnitApp _unidadeApp;
    private readonly IMapper _mapper;

    public UnitController(IUnitApp unidadeApp, IMapper mapper)
    {
        _unidadeApp = unidadeApp;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(List<UnitResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<List<UnitResponse>>> Get([FromQuery] UnitFilterRequest filtro, CancellationToken cancellationToken)
    {
        try
        {            
            var response = await _unidadeApp.Query(filtro, cancellationToken);
            if (response is not null && response.Any())
                return Ok(_mapper.Map<List<UnitResponse>>(response));

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(UnitResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitResponse>> Get(int id)
    {
        try
        {
            var unidade = _mapper.Map<UnitResponse>(await _unidadeApp.Get(id));
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

    [ProducesResponseType(typeof(UnitResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<UnitResponse>> Insert(UnitRequest unidadeRequest)
    {
        try
        {
            var unidade = await _unidadeApp.Insert(_mapper.Map<Unit>(unidadeRequest));
            if (unidade.IsValid)
            {
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<UnitResponse>(unidade));
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

    [ProducesResponseType(typeof(UnitResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitResponse>> Update(int id, UnitUpdateRequest unidadeRequest)
    {
        try
        {
            var unidade = await _unidadeApp.Update(id, unidadeRequest);

            if (!unidade.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (!unidade.Item2.IsValid)
            {
                return Ok(_mapper.Map<UnitResponse>(unidade.Item2));
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

    [ProducesResponseType(typeof(UnitResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
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