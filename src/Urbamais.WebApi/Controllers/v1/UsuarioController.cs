using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.Interfaces.Identity;
using Urbamais.Application.ViewModels.Request.v1.User;
using Urbamais.Application.ViewModels.Response.v1.User;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UsuarioController : ControllerBase
{
    private IIdentityAppService _identityService;

    public UsuarioController(IIdentityAppService identityService) =>
        _identityService = identityService;

    /// <summary>
    /// Cadastro de usuário.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="usuarioCadastro">Dados de cadastro do usuário</param>
    /// <returns></returns>
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("cadastro-usuario")]
    [Authorize]
    public async Task<IActionResult> Cadastrar(UserRegisterRequest usuarioCadastro)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var resultado = await _identityService.RegisterUser(usuarioCadastro);
        if (resultado.Success)
            return Ok(resultado);
        else if (resultado.Errors.Count > 0)
        {
            var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Errors);
            return BadRequest(problemDetails);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Login do usuário via usuário/senha.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="usuarioLogin">Dados de login do usuário</param>
    /// <returns></returns>
    /// <response code="200">Login realizado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Erro caso usuário não esteja autorizado</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("login")]
    public async Task<ActionResult<UserRegisterResponse>> Login(UserLoginRequest usuarioLogin)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var resultado = await _identityService.Login(usuarioLogin);
        if (resultado.Success)
            return Ok(resultado);

        return Unauthorized();
    }

    /// <summary>
    /// Login do usuário via refresh token.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Login realizado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Erro caso usuário não esteja autorizado</response>
    /// <response code="500">Retorna erros caso ocorram</response>
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
}