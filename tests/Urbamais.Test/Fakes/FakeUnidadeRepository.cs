using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Domain.InterfacesRepositories.Planejamento;

namespace Urbamais.Test.Fakes;
public sealed class FakeUnidadeRepository : IUnidadeRepository
{
    private readonly List<Unidade> _data = [];
    private int _nextId = 1;

    public Task AddAsync(Unidade entity, CancellationToken ct = default)
    {
        entity.GetType().GetProperty("Id")?.SetValue(entity, _nextId++);
        _data.Add(entity);
        return Task.CompletedTask;
    }

    public void Update(Unidade entity)
    {
        // No-op: a entidade já está referenciada na lista
    }

    public void Remove(Unidade entity)
    {
        _data.RemoveAll(x => x.Id == entity.Id);
    }

    public Task<Unidade?> GetByIdAsync(int id, CancellationToken ct = default)
        => Task.FromResult(_data.FirstOrDefault(x => x.Id == id));

    public Task<Unidade?> ObterPorSiglaAsync(string sigla, CancellationToken ct = default)
    {
        var s = (sigla ?? string.Empty).Trim().ToUpperInvariant();
        return Task.FromResult(_data.FirstOrDefault(x => x.Sigla == s));
    }

    public Task<IReadOnlyList<Unidade>> BuscarPorDescricaoAsync(string termo, int take = 50, CancellationToken ct = default)
    {
        termo = (termo ?? string.Empty).Trim();
        var result = _data
            .Where(x => x.Descricao?.Contains(termo, StringComparison.OrdinalIgnoreCase) == true)
            .OrderBy(x => x.Descricao)
            .Take(Math.Clamp(take, 1, 200))
            .ToList()
            .AsReadOnly();
        return Task.FromResult((IReadOnlyList<Unidade>)result);
    }

    public Task<bool> ExisteSiglaAsync(string sigla, CancellationToken ct = default)
    {
        var s = (sigla ?? string.Empty).Trim().ToUpperInvariant();
        return Task.FromResult(_data.Any(x => x.Sigla == s));
    }
}