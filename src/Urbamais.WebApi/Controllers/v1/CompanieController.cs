using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.App.Interfaces.Construction;
using Urbamais.Application.ViewModels.Request.V1.Companie;
using Urbamais.Application.ViewModels.Response.V1.Companie;
using Urbamais.Application.ViewModels.Response.V1.Unit;
using Urbamais.Domain.Entities.Obras;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class CompanieController : ControllerBase
{
    private readonly ICompanieApp _CompanieApp;
    private readonly IMapper _mapper;
    private readonly string _nameController = "Companie";

    public CompanieController(ICompanieApp CompanieApp, IMapper mapper)
    {
        _CompanieApp = CompanieApp;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(List<CompanieResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<List<CompanieResponse>>> Get([FromQuery] CompanieFilterRequest filtro, CancellationToken cancellationToken)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.READ))
                return Unauthorized();

            var response = await _CompanieApp.Query(filtro, cancellationToken);

            if (response is not null && response.Any())
                return Ok(_mapper.Map<List<CompanieResponse>>(response));

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(CompanieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CompanieResponse>> Get(int id)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.READ))
                return Unauthorized();

            var Companie = _mapper.Map<CompanieResponse>(await _CompanieApp.Get(id));
            if (Companie is not null)
                return Ok(Companie);

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(CompanieResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CompanieResponse>> Insert(CompanieRequest CompanieRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.CREATE))
                return Unauthorized();

            CompanieRequest.IdUserCreation = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var Companie = await _CompanieApp.Insert(_mapper.Map<Empresa>(CompanieRequest));
            if (Companie.IsValid)
            {
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<CompanieResponse>(Companie));
            }

            var problemDetail =
                new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                errors: Companie.ValidationResult!.Errors.Select(x => x.ErrorMessage));

            return BadRequest(problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(CompanieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CompanieResponse>> Update(int id, CompanieUpdateRequest CompanieRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.UPDATE))
                return Unauthorized();

            CompanieRequest.IdUserModification = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var Companie = await _CompanieApp.Update(id, CompanieRequest);

            if (!Companie.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (!Companie.Item2.IsValid)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                    errors: Companie.Item2.ValidationResult!.Errors.Select(x => x.ErrorMessage));

                return BadRequest(problemDetail);
            }

            return Ok(_mapper.Map<CompanieResponse>(Companie.Item2));
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
            var Companie = await _CompanieApp.Delete(id, IdUserDeletion!);

            if (!Companie.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (((CompanieResponse)Companie.Item2).Success)
            {
                return NoContent();
            }

            var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: ((UnitResponse)Companie.Item2).Errors);
            return StatusCode(400, problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }
}