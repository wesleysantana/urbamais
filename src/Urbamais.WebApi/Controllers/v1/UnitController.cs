using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.App.Interfaces.Planning;
using Urbamais.Application.ViewModels.Request.V1.Unit;
using Urbamais.Application.ViewModels.Response.V1.Unit;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class UnitController : ControllerBase
{
    private readonly IUnitApp _unitApp;
    private readonly IMapper _mapper;
    private readonly string _nameController = "Unit";

    public UnitController(IUnitApp unitApp, IMapper mapper)
    {
        _unitApp = unitApp;
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
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.READ)) 
                return Unauthorized();

            var response = await _unitApp.Query(filtro, cancellationToken);

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
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.READ))
                return Unauthorized();

            var unit = _mapper.Map<UnitResponse>(await _unitApp.Get(id));
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

    [ProducesResponseType(typeof(UnitResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<UnitResponse>> Insert(UnitRequest unitRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.CREATE))
                return Unauthorized();

            unitRequest.IdUserCreation = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var unit = await _unitApp.Insert(_mapper.Map<Unidade>(unitRequest));
            if (unit.IsValid)
            {
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<UnitResponse>(unit));
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

    [ProducesResponseType(typeof(UnitResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitResponse>> Update(int id, UnitUpdateRequest unitRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.UPDATE))
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

            return Ok(_mapper.Map<UnitResponse>(unit.Item2));
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
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.DELETE))
                return Unauthorized();

            var IdUserDeletion = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var unit = await _unitApp.Delete(id, IdUserDeletion!);

            if (!unit.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (((UnitResponse)unit.Item2).Success)
            {
                return NoContent();
            }

            var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: ((UnitResponse)unit.Item2).Errors);
            return StatusCode(400, problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }
}