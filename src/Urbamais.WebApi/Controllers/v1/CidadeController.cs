using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class CidadeController : ControllerBase
{
    private readonly ICidadeAppService _app;
    public CidadeController(ICidadeAppService app) => _app = app;

    private const int DEFAULT_PAGE_SIZE = 50;
    private const int MAX_PAGE_SIZE = 200;

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Cidade), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var c = await _app.ObterPorIdAsync(id, ct);
        return c is null ? NotFound() : Ok(c);
    }

    [HttpGet("por-uf/{uf}")]
    [ProducesResponseType(typeof(List<Cidade>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BuscarPorUf(string uf, CancellationToken ct)
    {
        if (!Enum.TryParse<Uf>(uf, true, out var parsed))
            return Problem(statusCode: 400, detail: "UF inválida.");

        var list = await _app.BuscarPorUfAsync(parsed, ct);
        return Ok(list);
    }

    [HttpGet("buscar")]
    [ProducesResponseType(typeof(List<Cidade>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar(
        [FromQuery] string nome, 
        [FromQuery] string uf = "", 
        [FromQuery, Range(1, 200)] int take = DEFAULT_PAGE_SIZE, 
        CancellationToken ct = default)
    {
        var pageSize = Math.Clamp(take <= 0 ? DEFAULT_PAGE_SIZE : take, 1, MAX_PAGE_SIZE);
        Uf parsed = 0;
        var hasUf = !string.IsNullOrWhiteSpace(uf);
        if (hasUf && !Enum.TryParse<Uf>(uf, true, out parsed))
            return Problem(statusCode: 400, detail: "UF inválida.");

        var list = hasUf
            ? await _app.BuscarPorNomeEUfAsync(nome ?? string.Empty, parsed, pageSize, ct)
            : await _app.BuscarPorNomeAsync(nome ?? string.Empty, pageSize, ct);

        return Ok(list);
    }
}