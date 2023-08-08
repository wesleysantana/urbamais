using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.Interfaces.Identity;
using Urbamais.Application.ViewModels.Request.V1.User;
using Urbamais.Application.ViewModels.Response.V1.Unidade;
using Urbamais.Application.ViewModels.Response.V1.User;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UsuarioController : ControllerBase
{
    private readonly IIdentityAppService _identityService;

    public UsuarioController(IIdentityAppService identityService)
    {
        _identityService = identityService;
    }

    [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<UnidadeResponse>>> Get(CancellationToken cancellationToken)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.READ))
                return Unauthorized();

            var response = await _identityService.GetUsers(cancellationToken);
            if (response is not null && response.Any())
                return Ok(response);

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpPost("register-user")]
    public async Task<ActionResult<UserRegisterResponse>> RegisterUser(UserRegisterRequest userRegister)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.CREATE))
                return Unauthorized();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _identityService.RegisterUser(userRegister, userId!);

            if (!result.Item1)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: result.Item2.Errors);
                return BadRequest(problemDetails);
            }

            if (result.Item2.Errors.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: result.Item2.Errors);
                return BadRequest(problemDetails);
            }

            return Ok(result.Item2);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponse>> Update(string id, UserUpdateRequest userUpdateRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.UPDATE))
                return Unauthorized();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _identityService.UpdateUser(id, userUpdateRequest, userId!);

            if (!user.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (!user.Item2.Success)
            {
                var problemDetail =
                new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request, errors: user.Item2.Errors);

                return BadRequest(problemDetail);
            }

            return Ok(user.Item2);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("login")]
    public async Task<ActionResult<UserRegisterResponse>> Login(UserLoginRequest userLogin)
    {
        var result = await _identityService.Login(userLogin);
        if (result.Success)
        {
            ListPerfis.Instance.Claims = userLogin.Claims;
            return Ok(result);
        }

        return Unauthorized();
    }

    [ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpPost("refresh-login")]
    public async Task<ActionResult<UserRegisterResponse>> RefreshLogin()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var usuarioId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId == null)
            return BadRequest();

        var resultado = await _identityService.RefreshLogin(usuarioId);
        if (resultado.Success)
            return Ok(resultado);

        return Unauthorized();
    }

    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpDelete("delete-user")]
    public async Task<ActionResult<UserResponse>> DeleteUser(string userIdDelete)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.DELETE))
                return Unauthorized();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _identityService.DeleteUser(userIdDelete, userId!);

            if (!result.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (result.Item2.Errors.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: result.Item2.Errors);
                return BadRequest(problemDetails);
            }

            return Ok(result.Item2);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }
}