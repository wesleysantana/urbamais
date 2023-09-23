using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.App.Interfaces.Core;
using Urbamais.Application.ViewModels.Request.V1.Cidade;
using Urbamais.Application.ViewModels.Response.V1.Cidade;
using Urbamais.Application.ViewModels.Response.V1.Unidade;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class CidadeController : ControllerBase
{
    private readonly ICidadeApp _cityApp;
    private readonly IMapper _mapper;

    public CidadeController(ICidadeApp CityApp, IMapper mapper)
    {
        _cityApp = CityApp;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(List<CidadeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<List<CidadeResponse>>> Get([FromQuery] CidadeFilterRequest filtro, CancellationToken cancellationToken)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.READ))
                return Unauthorized();

            var response = await _cityApp.Query(filtro, cancellationToken);

            if (response is null || !response.Any())
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));

            return Ok(_mapper.Map<List<CidadeResponse>>(response));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(CidadeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CidadeResponse>> Get(int id)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.READ))
                return Unauthorized();

            var city = _mapper.Map<CidadeResponse>(await _cityApp.Get(id));

            if (city is null)
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));

            return Ok(city);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(CidadeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CidadeResponse>> Insert(CidadeRequest cityRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.CREATE))
                return Unauthorized();

            cityRequest.IdUserCreation = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var city = await _cityApp.Insert(_mapper.Map<Cidade>(cityRequest));

            if (!city.IsValid)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                    errors: city.ValidationResult!.Errors.Select(x => x.ErrorMessage));

                return BadRequest(problemDetail);
            }

            return StatusCode((int)HttpStatusCode.Created, _mapper.Map<CidadeResponse>(city));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(CidadeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CidadeResponse>> Update(int id, CidadeUpdateRequest cityRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.UPDATE))
                return Unauthorized();

            cityRequest.IdUserModification = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var cidadeUpdate = await _cityApp.Update(id, cityRequest);

            if (cidadeUpdate.Item1 == HttpStatusCode.NotFound)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            var cidade = (CidadeResponse)cidadeUpdate.Item2;

            if (!cidade.Success)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request, errors: cidade.Errors);
                return BadRequest(problemDetail);
            }

            return Ok(_mapper.Map<CidadeResponse>(cidade));
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
            var cidadeDelete = await _cityApp.Delete(id, IdUserDeletion!);

            if (cidadeDelete.Item1 == HttpStatusCode.NotFound)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            var cidade = (CidadeResponse)cidadeDelete.Item2;
            if (!cidade.Success)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: cidade.Errors);
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