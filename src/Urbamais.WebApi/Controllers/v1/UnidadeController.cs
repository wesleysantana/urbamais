using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Application.ViewModels.Response.v1.Unidade;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UnidadeController : ControllerBase
{
    private readonly IUnidadeAppService _app;
    private readonly IMapper _mapper;

    public UnidadeController(IUnidadeAppService app, IMapper mapper)
    {
        _app = app;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var u = await _app.ObterPorIdAsync(id, ct);
        return u is null ? NotFound() : Ok(_mapper.Map<UnidadeResponse>(u));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<UnidadeResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] string? descricao, [FromQuery] int take = 50, CancellationToken ct = default)
    {
        var list = await _app.BuscarPorDescricaoAsync(descricao ?? "", take, ct);
        return Ok(_mapper.Map<List<UnidadeResponse>>(list));
    }

    [HttpPost]
    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(UnidadeRequest req, CancellationToken ct)
    {
        var r = await _app.CadastrarAsync(req, ct);
        if (!r.IsSuccess) return Problem(statusCode: 400, detail: string.Join("; ", r.Errors));
        var created = await _app.ObterPorIdAsync(r.Value, ct);
        return CreatedAtAction(nameof(GetById), new { id = r.Value }, _mapper.Map<UnidadeResponse>(created));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, UnidadeUpdateRequest req, CancellationToken ct)
    {
        var r = await _app.AtualizarAsync(id, req, ct);
        if (r.IsNotFound) return NotFound();
        if (!r.IsSuccess) return Problem(statusCode: 400, detail: string.Join("; ", r.Errors));
        var atual = await _app.ObterPorIdAsync(id, ct);
        return Ok(_mapper.Map<UnidadeResponse>(atual));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var r = await _app.ExcluirAsync(id, ct);
        return r.IsNotFound ? NotFound() : NoContent();
    }
}