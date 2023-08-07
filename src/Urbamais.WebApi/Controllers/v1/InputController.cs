using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.App.Interfaces.Planning;
using Urbamais.Application.ViewModels.Request.V1.Input;
using Urbamais.Application.ViewModels.Response.V1.Input;
using Urbamais.Application.ViewModels.Response.V1.Unit;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.V1;
[Route("api/[controller]")]
[ApiController]
public class InputController : ControllerBase
{
    private readonly IInputApp _InputApp;
    private readonly IMapper _mapper;
    private readonly string _nameController = "Input";

    public InputController(IInputApp InputApp, IMapper mapper)
    {
        _InputApp = InputApp;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(List<InputResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<List<InputResponse>>> Get([FromQuery] InputFilterRequest filtro, CancellationToken cancellationToken)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.READ))
                return Unauthorized();

            var response = await _InputApp.Query(filtro, cancellationToken);

            if (response is not null && response.Any())
                return Ok(_mapper.Map<List<InputResponse>>(response));

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(InputResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<InputResponse>> Get(int id)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.READ))
                return Unauthorized();

            var Input = _mapper.Map<InputResponse>(await _InputApp.Get(id));
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

    [ProducesResponseType(typeof(InputResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<InputResponse>> Insert(InputRequest InputRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.CREATE))
                return Unauthorized();

            InputRequest.IdUserCreation = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var Input = await _InputApp.Insert(_mapper.Map<Insumo>(InputRequest));
            if (Input.IsValid)
            {
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<InputResponse>(Input));
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

    [ProducesResponseType(typeof(InputResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<InputResponse>> Update(int id, InputUpdateRequest InputRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.UPDATE))
                return Unauthorized();

            InputRequest.IdUserModification = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var Input = await _InputApp.Update(id, InputRequest);

            if (!Input.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }
            
            if (!Input.Item2.IsValid)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                    errors: Input.Item2.ValidationResult!.Errors.Select(x => x.ErrorMessage));

                return BadRequest(problemDetail);
            }

            return Ok(_mapper.Map<InputResponse>(Input.Item2));
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
            var input = await _InputApp.Delete(id, IdUserDeletion!);

            if (!input.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (((InputResponse)input.Item2).Success)
            {
                return NoContent();
            }

            var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: ((UnitResponse)input.Item2).Errors);
            return StatusCode(400, problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }
}
