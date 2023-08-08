using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.App.Interfaces.Planejamentos;
using Urbamais.Application.ViewModels.Request.V1.Unidade;
using Urbamais.Application.ViewModels.Response.V1.Unidade;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class UnidadeController : ControllerBase
{
    private readonly IUnidadeApp _unitApp;
    private readonly IMapper _mapper;

    public UnidadeController(IUnidadeApp unitApp, IMapper mapper)
    {
        _unitApp = unitApp;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(List<UnidadeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<List<UnidadeResponse>>> Get([FromQuery] UnidadeFilterRequest filtro, CancellationToken cancellationToken)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.READ)) 
                return Unauthorized();

            var response = await _unitApp.Query(filtro, cancellationToken);

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

    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UnidadeResponse>> Get(int id)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.READ))
                return Unauthorized();

            var unit = _mapper.Map<UnidadeResponse>(await _unitApp.Get(id));
            if (unit is not null)
                return Ok(unit);

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<UnidadeResponse>> Insert(UnidadeRequest unitRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.CREATE))
                return Unauthorized();

            unitRequest.IdUserCreation = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var unit = await _unitApp.Insert(_mapper.Map<Unidade>(unitRequest));
            if (unit.IsValid)
            {
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<UnidadeResponse>(unit));
            }

            var problemDetail =
                new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                errors: unit.ValidationResult!.Errors.Select(x => x.ErrorMessage));

            return BadRequest(problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnidadeResponse>> Update(int id, UnidadeUpdateRequest unitRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.UPDATE))
                return Unauthorized();

            unitRequest.IdUserModification = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var unit = await _unitApp.Update(id, unitRequest);

            if (!unit.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (!unit.Item2.IsValid)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                    errors: unit.Item2.ValidationResult!.Errors.Select(x => x.ErrorMessage));

                return BadRequest(problemDetail);
            }

            return Ok(_mapper.Map<UnidadeResponse>(unit.Item2));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.DELETE))
                return Unauthorized();

            var IdUserDeletion = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var unit = await _unitApp.Delete(id, IdUserDeletion!);

            if (!unit.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (((UnidadeResponse)unit.Item2).Success)
            {
                return NoContent();
            }

            var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: ((UnidadeResponse)unit.Item2).Errors);
            return StatusCode(400, problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }
}