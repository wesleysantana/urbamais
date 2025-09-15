using Core.Domain;
using Core.ValueObjects;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Test.Fakes;
public sealed class FakeCidadeAppService : ICidadeAppService
{
    private readonly List<Cidade> _db = [];

    public FakeCidadeAppService()
    {
        // Seed mínimo
        _db.Add(new Cidade(new NomeVO("Rio de Janeiro"), Uf.RJ));
        _db.Add(new Cidade(new NomeVO("São Paulo"), Uf.SP));
    }

    public Task<Cidade?> ObterPorIdAsync(int id, CancellationToken ct = default)
        => Task.FromResult(_db.FirstOrDefault(x => x.Id == id));

    // NOVA ASSINATURA conforme sua alteração: BuscarPorUf (com take)
    public Task<IReadOnlyList<Cidade>> BuscarPorUfAsync(Uf uf, CancellationToken ct = default)
    {
        var list = _db.Where(x => x.Uf == uf)
                      .OrderBy(x => x.Nome.Nome)
                      .Take(Math.Clamp(200, 1, 1000))
                      .ToList()
                      .AsReadOnly();
        return Task.FromResult((IReadOnlyList<Cidade>)list);
    }

    public Task<IReadOnlyList<Cidade>> BuscarPorNomeEUfAsync(string nome, Uf uf, int take = 50, CancellationToken ct = default)
    {
        var n = (nome ?? string.Empty).Trim();
        var list = _db.Where(x => x.Uf == uf && x.Nome.Nome.Contains(n, StringComparison.OrdinalIgnoreCase))
                      .OrderBy(x => x.Nome.Nome)
                      .Take(Math.Clamp(take, 1, 1000))
                      .ToList()
                      .AsReadOnly();
        return Task.FromResult((IReadOnlyList<Cidade>)list);
    }

    public Task<IReadOnlyList<Cidade>> BuscarPorNomeAsync(string nome, int take = 50, CancellationToken ct = default)
    {
        var n = (nome ?? string.Empty).Trim();
        var list = _db.Where(x => x.Nome.Nome.Contains(n, StringComparison.OrdinalIgnoreCase))
                      .OrderBy(x => x.Nome.Nome)
                      .Take(Math.Clamp(take, 1, 1000))
                      .ToList()
                      .AsReadOnly();
        return Task.FromResult((IReadOnlyList<Cidade>)list);
    }
}
