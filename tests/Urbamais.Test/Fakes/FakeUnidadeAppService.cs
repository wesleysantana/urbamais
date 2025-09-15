using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Shared;
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.Test.Fakes;
public sealed class FakeUnidadeAppService : IUnidadeAppService
{
    private readonly List<Unidade> _db = [];
    private int _nextId = 1;

    public Task<Unidade?> ObterPorIdAsync(int id, CancellationToken ct = default)
        => Task.FromResult(_db.FirstOrDefault(x => x.Id == id));

    public Task<Unidade?> ObterPorSiglaAsync(string sigla, CancellationToken ct = default)
        => Task.FromResult(_db.FirstOrDefault(x => x.Sigla == sigla));

    public Task<IReadOnlyList<Unidade>> BuscarPorDescricaoAsync(string termo, int take = 50, CancellationToken ct = default)
    {
        var list = _db.Where(x => x.Descricao.Contains(termo))
                      .OrderBy(x => x.Descricao)
                      .Take(take)
                      .ToList()
                      .AsReadOnly();
        return Task.FromResult((IReadOnlyList<Unidade>)list);
    }

    public Task<Result<int>> CadastrarAsync(UnidadeRequest request, CancellationToken ct = default)
    {
        var descricao = request.Descricao ?? string.Empty;
        var sigla = request.Sigla ?? string.Empty;
        var u = new Unidade(descricao, sigla);
        u.GetType().GetProperty("Id")?.SetValue(u, _nextId++);
        _db.Add(u);
        return Task.FromResult(Result<int>.Ok(u.Id));
    }

    public Task<Result> AtualizarAsync(int id, UnidadeUpdateRequest request, CancellationToken ct = default)
    {
        var u = _db.FirstOrDefault(x => x.Id == id);
        if (u is null) return Task.FromResult(Result.NotFound());
        u.Update(request.Descricao, request.Sigla);
        return Task.FromResult(Result.Ok());
    }

    public Task<Result> ExcluirAsync(int id, CancellationToken ct = default)
    {
        var u = _db.FirstOrDefault(x => x.Id == id);
        if (u is null) return Task.FromResult(Result.NotFound());
        u.Delete();
        return Task.FromResult(Result.Ok());
    }

    public Task<Result> ExcluirDefinitivoAsync(int id, CancellationToken ct = default)
    {
        _db.RemoveAll(x => x.Id == id);
        return Task.FromResult(Result.Ok());
    }

    public Task<Result> RestaurarAsync(int id, CancellationToken ct = default)
    {
        var u = _db.FirstOrDefault(x => x.Id == id);
        if (u is null) return Task.FromResult(Result.NotFound());
        u.Restore();
        return Task.FromResult(Result.Ok());
    }
}
