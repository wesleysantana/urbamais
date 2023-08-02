using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Urbamais.Application.App.Interfaces.Core;
using Urbamais.Application.ViewModels.Request.V1.City;
using Urbamais.Application.ViewModels.Response.V1.City;
using Urbamais.Application.ViewModels.Response.V1.Unit;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly ICityApp _cityApp;
    private readonly IMapper _mapper;
    private readonly string _nameController = "City";

    public CityController(ICityApp CityApp, IMapper mapper)
    {
        _cityApp = CityApp;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(List<CityResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<List<CityResponse>>> Get([FromQuery] CityFilterRequest filtro, CancellationToken cancellationToken)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.READ))
                return Unauthorized();

            var response = await _cityApp.Query(filtro, cancellationToken);

            if (response is not null && response.Any())
                return Ok(_mapper.Map<List<CityResponse>>(response));

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(CityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CityResponse>> Get(int id)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.READ))
                return Unauthorized();

            var city = _mapper.Map<CityResponse>(await _cityApp.Get(id));
            if (city is not null)
                return Ok(city);

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(CityResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CityResponse>> Insert(CityRequest cityRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.CREATE))
                return Unauthorized();

            cityRequest.IdUserCreation = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var city = await _cityApp.Insert(_mapper.Map<City>(cityRequest));
            if (city.IsValid)
            {
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<CityResponse>(city));
            }

            var problemDetail =
                new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                errors: city.ValidationResult!.Errors.Select(x => x.ErrorMessage));

            return BadRequest(problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    [ProducesResponseType(typeof(CityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CityResponse>> Update(int id, CityUpdateRequest cityRequest)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, _nameController, Constants.UPDATE))
                return Unauthorized();

            cityRequest.IdUserModification = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;

            var city = await _cityApp.Update(id, cityRequest);

            if (!city.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (!city.Item2.IsValid)
            {
                var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                    errors: city.Item2.ValidationResult!.Errors.Select(x => x.ErrorMessage));

                return BadRequest(problemDetail);
            }

            return Ok(_mapper.Map<CityResponse>(city.Item2));
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
            var city = await _cityApp.Delete(id, IdUserDeletion!);

            if (!city.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (((CityResponse)city.Item2).Success)
            {
                return NoContent();
            }

            var problemDetail = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: ((UnitResponse)city.Item2).Errors);
            return StatusCode(400, problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }
}