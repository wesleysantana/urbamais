using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using Urbamais.Application.Interfaces.Identity;
using Urbamais.Application.ViewModels.Request.v1.Role;
using Urbamais.Application.ViewModels.Response.v1.Role;
using Urbamais.WebApi.ControllersHelper;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly IIdentityAppService _identityService;
    private readonly string _nameController = "Role";

    public RoleController(IIdentityAppService identityService)
    {
        _identityService = identityService;
    }

    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    [HttpGet("get_list_controllers")]
    public ActionResult<List<string>> GetlistControllers()
    {
        if (!AuthorizeAccess.Valid(_nameController, Constants.READ))
            return Unauthorized();

        return Ok(ListControllers.Instance.List.ToList());
    }

    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("get-users-in-role")]
    public async Task<ActionResult<RoleResponse>> GetUsersInRole(string roleName)
    {
        try
        {
            if (!AuthorizeAccess.Valid(_nameController, Constants.READ))
                return Unauthorized();

            var result = await _identityService.GetUsersInRole(roleName);

            if (!result.Item1)
                return NotFound();

            if (result.Item2 is null)
                return BadRequest();

            return Ok(result.Item2);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("register-role")]
    public async Task<ActionResult<RoleResponse>> RegisterRole(RoleRequest roleRegister)
    {
        try
        {
            if (!AuthorizeAccess.Valid(_nameController, Constants.CREATE))
                return Unauthorized();

            var validRoleAndClaims = ValidRoleAndClaims(roleRegister.Claims);
            if (validRoleAndClaims is not null)
                return BadRequest(validRoleAndClaims);

            var result = await _identityService.RegisterRole(roleRegister);

            if (result.Errors.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: result.Errors);
                return BadRequest(problemDetails);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("update-role")]
    public async Task<ActionResult<RoleResponse>> UpdateRole(string name, RoleUpdateRequest roleUpdate)
    {
        try
        {

            if (!AuthorizeAccess.Valid(_nameController, Constants.UPDATE))
                return Unauthorized();

            var validRoleAndClaims = ValidRoleAndClaims(roleUpdate.Claims!);
            if (validRoleAndClaims is not null)
                return BadRequest(validRoleAndClaims);

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

            if (!AuthorizeAccess.Valid(_nameController, Constants.DELETE))
                return Unauthorized();

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

    private CustomProblemDetails? ValidRoleAndClaims(IDictionary<string, string> roleRegister)
    {
        if (roleRegister.Keys.Any(key => !ListControllers.Instance.List.Contains(key)))
        {
            return new CustomProblemDetails(HttpStatusCode.BadRequest,
                errors: new List<string> { @"Foi infomado Controller(s) inexistente(s) para criação do Perfil." });
        }

        foreach (var kvp in roleRegister)
        {
            string key = kvp.Key;
            string value = kvp.Value;

            /*
                ^ indica o início da string.
                | representa uma alternativa, neste caso, uma string vazia.
                [CRUD] é uma classe de caracteres que permite apenas as letras C, R, U e D.
                {1,4} define que essa classe de caracteres pode aparecer de 1 a 4 vezes.
                $ indica o fim da string.
            */
            bool isValid = Regex.IsMatch(value, @"^(|[CRUD]{1,4})$");

            if (!isValid)
            {
                return new CustomProblemDetails(HttpStatusCode.BadRequest,
                    errors: new List<string> { @"Os valores de permissão devem ser 'C', 'R', 'U' ou 'D'." });
            }
        }

        return null;
    }
}