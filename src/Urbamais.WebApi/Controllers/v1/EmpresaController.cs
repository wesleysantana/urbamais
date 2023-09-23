using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.App.Interfaces.Obras;
using Urbamais.Application.ViewModels.Request.V1.Empresa;
using Urbamais.Application.ViewModels.Response.V1.Empresa;
using Urbamais.Application.ViewModels.Response.V1.Unidade;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Obras;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaApp _CompanieApp;
    private readonly IMapper _mapper;

    public EmpresaController(IEmpresaApp CompanieApp, IMapper mapper)
    {
        _CompanieApp = CompanieApp;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(List<EmpresaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<List<EmpresaResponse>>> Get([FromQuery] EmpresaFilterRequest filtro, CancellationToken cancellationToken)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.READ))
                return Unauthorized();

            var response = await _CompanieApp.Query(filtro, cancellationToken);

            if (response is not null && response.Any())
                return Ok(_mapper.Map<List<EmpresaResponse>>(response));

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(EmpresaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<EmpresaResponse>> Get(int id)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.READ))
                return Unauthorized();

            var Companie = _mapper.Map<EmpresaResponse>(await _CompanieApp.Get(id));
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

    [ProducesResponseType(typeof(EmpresaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<EmpresaResponse>> Insert(EmpresaRequest CompanieRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.CREATE))
                return Unauthorized();

            CompanieRequest.IdUserCreation = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var Companie = await _CompanieApp.Insert(_mapper.Map<Empresa>(CompanieRequest));
            if (Companie.IsValid)
            {
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<EmpresaResponse>(Companie));
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

    [ProducesResponseType(typeof(EmpresaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<EmpresaResponse>> Update(int id, EmpresaUpdateRequest empresaRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.UPDATE))
                return Unauthorized();

            empresaRequest.IdUserModification = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var empresaUpdate = await _CompanieApp.Update(id, empresaRequest);

            if (empresaUpdate.Item1 == HttpStatusCode.NotFound)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            var empresa = (EmpresaResponse)empresaUpdate.Item2;
            if (!empresa.Success)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request, errors: empresa.Errors);
                return BadRequest(problemDetail);
            }

            return Ok(_mapper.Map<EmpresaResponse>(empresa));
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
            var empresaDelete = await _CompanieApp.Delete(id, IdUserDeletion!);

            if (empresaDelete.Item1 == HttpStatusCode.NotFound)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            var empresa = (EmpresaResponse)empresaDelete.Item2;
            if (!empresa.Success)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: empresa.Errors);
                return StatusCode(400, problemDetail);                
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