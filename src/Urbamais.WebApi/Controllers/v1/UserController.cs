using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;
using Urbamais.Application.Interfaces.Identity;
using Urbamais.Application.ViewModels.Request.v1.Role;
using Urbamais.Application.ViewModels.Request.v1.User;
using Urbamais.Application.ViewModels.Response.v1.Role;
using Urbamais.Application.ViewModels.Response.v1.User;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UserController : ControllerBase
{
    private IIdentityAppService _identityService;

    public UserController(IIdentityAppService identityService) =>
        _identityService = identityService;

    //[ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    //[HttpPost("register-user")]
    //[Authorize]
    //public async Task<IActionResult> RegisterUser(UserRegisterRequest userRegister)
    //{
    //    var result = await _identityService.RegisterUser(userRegister);
    //    if (result.Success)
    //        return Ok(result);
    //    else if (result.Errors.Count > 0)
    //    {
    //        var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: result.Errors);
    //        return BadRequest(problemDetails);
    //    }

    //    return StatusCode(StatusCodes.Status500InternalServerError);
    //}

    [ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("register-user")]
    public async Task<ActionResult<UserRegisterResponse>> RegisterUser(UserRegisterRequest userRegister)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _identityService.RegisterUser(userRegister, userId!);       

        if(!result.Item2)
        {
            var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: result.Item1.Errors);
            return BadRequest(problemDetails);
        }

        if (result.Item1.Success)
            return Ok(result.Item1);
        else if (result.Item1.Errors.Count > 0)
        {
            var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: result.Item1.Errors);
            return BadRequest(problemDetails);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
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
            return Ok(result);

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

    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("register-role")]
    public async Task<ActionResult<RoleResponse>> RegisterRole(RoleRequest roleRegister)
    {
        var result = await _identityService.RegisterRole(roleRegister);
        if (result.Success)
            return Ok(result);
        else if (result.Errors.Count > 0)
        {
            var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: result.Errors);
            return BadRequest(problemDetails);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("update-role")]
    public async Task<ActionResult<RoleResponse>> UpdateRole(string name, RoleRequest roleUpdate)
    {
        try
        {
            var result = await _identityService.UpdateRole(name, roleUpdate);

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

    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpDelete("delete-role")]
    public async Task<ActionResult<RoleResponse>> DeleteRole(RoleRequest roleDelete)
    {
        try
        {
            var result = await _identityService.DeleteRole(roleDelete.Name);

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