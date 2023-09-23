using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.App.Interfaces.Planejamentos;
using Urbamais.Application.ViewModels.Request.V1.Insumo;
using Urbamais.Application.ViewModels.Response.V1.Insumo;
using Urbamais.Application.ViewModels.Response.V1.Unidade;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.V1;
[Route("api/[controller]")]
[ApiController]
public class InsumoController : ControllerBase
{
    private readonly IInsumoApp _InputApp;
    private readonly IMapper _mapper;

    public InsumoController(IInsumoApp InputApp, IMapper mapper)
    {
        _InputApp = InputApp;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(List<InsumoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<List<InsumoResponse>>> Get([FromQuery] InsumoFilterRequest filtro, CancellationToken cancellationToken)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.READ))
                return Unauthorized();

            var response = await _InputApp.Query(filtro, cancellationToken);

            if (response is not null && response.Any())
                return Ok(_mapper.Map<List<InsumoResponse>>(response));

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(InsumoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<InsumoResponse>> Get(int id)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.READ))
                return Unauthorized();

            var Input = _mapper.Map<InsumoResponse>(await _InputApp.Get(id));
            if (Input is not null)
                return Ok(Input);

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(InsumoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<InsumoResponse>> Insert(InsumoRequest InputRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.CREATE))
                return Unauthorized();

            InputRequest.IdUserCreation = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var Input = await _InputApp.Insert(_mapper.Map<Insumo>(InputRequest));
            if (Input.IsValid)
            {
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<InsumoResponse>(Input));
            }

            var problemDetail =
                new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                errors: Input.ValidationResult!.Errors.Select(x => x.ErrorMessage));

            return BadRequest(problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(InsumoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<InsumoResponse>> Update(int id, InsumoUpdateRequest insumoRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.UPDATE))
                return Unauthorized();

            insumoRequest.IdUserModification = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var insumoUpdate = await _InputApp.Update(id, insumoRequest);

            if (insumoUpdate.Item1 == HttpStatusCode.NotFound)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            var insumo = (InsumoResponse)insumoUpdate.Item2;
            if (!insumo.Success)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request, errors: insumo.Errors);
                return BadRequest(problemDetail);
            }

            return Ok(_mapper.Map<InsumoResponse>(insumo));
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
            var insumoDelete = await _InputApp.Delete(id, IdUserDeletion!);

            if (insumoDelete.Item1 == HttpStatusCode.NotFound)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            var insumo = (InsumoResponse)insumoDelete.Item2;
            if (!insumo.Success)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: insumo.Errors);
                return BadRequest(problemDetail);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }
}
