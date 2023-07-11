using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Urbamais.Application.App.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Application.ViewModels.Response.v1.Unidade;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UnidadeController : ControllerBase
{
    private readonly IUnidadeApp _unidadeApp;
    private readonly IMapper _mapper;

    public UnidadeController(IUnidadeApp unidadeApp, IMapper mapper)
    {
        _unidadeApp = unidadeApp;
        _mapper = mapper;
    }

    /// <summary>
    /// Lista Unidades
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="filtro">Filtros para consulta</param>
    /// <returns>Lista de Unidades cadastradas</returns>
    /// <response code="200">Unidade(s) retornada(s) com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Erro caso usuário não esteja autorizado</response>
    /// <response code="404">Nenhuma Unidade encontrada</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(List<UnidadeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<List<UnidadeResponse>>> Get([FromQuery] UnidadeFiltroRequest filtro, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _unidadeApp.Query(filtro, cancellationToken);
            if (response is not null && response.Any())
                return Ok(_mapper.Map<List<UnidadeResponse>>(response));

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    /// <summary>
    /// Consulta Unidade por ID
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="id">Identificador da Unidade</param>
    /// <returns>Retorna a unidade do ID informado</returns>
    /// <response code="200">Unidade retornada com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Erro caso usuário não esteja autorizado</response>
    /// <response code="404">Nenhuma Unidade encontrada</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UnidadeResponse>> Get(int id)
    {
        try
        {
            var unidade = _mapper.Map<UnidadeResponse>(await _unidadeApp.Get(id));
            if (unidade is not null)
                return Ok(unidade);

            return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    /// <summary>
    /// Insere uma nova Unidade
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="unidadeRequest">Dados da unidade</param>
    /// <returns>A Unidade cadastrada</returns>
    /// <response code="200">Unidade(s) retornada(s) com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Erro caso usuário não esteja autorizado</response>
    /// <response code="404">Nenhuma Unidade encontrada</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<UnidadeResponse>> Insert(UnidadeRequest unidadeRequest)
    {
        try
        {
            var unidade = await _unidadeApp.Insert(_mapper.Map<Unidade>(unidadeRequest));
            if (unidade.IsValid)
            {
                return StatusCode((int)HttpStatusCode.Created, _mapper.Map<UnidadeResponse>(unidade));
            }

            var problemDetail =
                new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                errors: unidade.ValidationResult!.Errors.Select(x => x.ErrorMessage));

            return BadRequest(problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    /// <summary>
    /// Altera dados de uma Unidade
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="id">Identificador da unidade</param>
    /// <param name="unidadeRequest">Dados a serem alterados</param>
    /// <returns>A Unidade alterada</returns>
    /// <response code="200">Unidade(s) retornada(s) com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Erro caso usuário não esteja autorizado</response>
    /// <response code="404">Nenhuma Unidade encontrada</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnidadeResponse>> Update(int id, UnidadeUpdateRequest unidadeRequest)
    {
        try
        {
            var unidade = await _unidadeApp.Update(id, unidadeRequest);

            if (!unidade.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (unidade.Item2.IsValid)
            {
                return Ok(_mapper.Map<UnidadeResponse>(unidade.Item2));
            }

            var problemDetail =
                new CustomProblemDetails(HttpStatusCode.BadRequest, request: Request,
                errors: unidade.Item2.ValidationResult!.Errors.Select(x => x.ErrorMessage));

            return BadRequest(problemDetail);
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }

    /// <summary>
    /// Deleta uma Unidade
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="id">Identificador da unidade</param>
    /// <response code="204">Unidade(s) retornada(s) com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Erro caso usuário não esteja autorizado</response>
    /// <response code="404">Nenhuma Unidade encontrada</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var unidade = await _unidadeApp.Delete(id);

            if (!unidade.Item1)
            {
                return NotFound(new CustomProblemDetails(HttpStatusCode.NotFound));
            }

            if (unidade.Item2)
            {
                return NoContent();
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            var problemDetail = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, detail: ex.Message);
            return StatusCode(500, problemDetail);
        }
    }
}