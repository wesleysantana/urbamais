using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Infra.Config;

namespace Urbamais.Infra.Repositories.Core;

public sealed class CidadeRepository : ICidadeRepository
{
    private readonly ContextEf _ctx;
    private DbSet<Cidade> Set => _ctx.Set<Cidade>();

    public CidadeRepository(ContextEf ctx)
    {
        _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
    }

    // ===== Escrita =====
    public Task AddAsync(Cidade entity, CancellationToken ct = default) => Set.AddAsync(entity, ct).AsTask();

    public void Update(Cidade entity) => Set.Update(entity);

    public void Remove(Cidade entity) => Set.Remove(entity); // hard; para soft: entity.Delete(); Update(entity);

    // ===== Leitura =====
    public Task<Cidade?> GetByIdAsync(int id, CancellationToken ct = default) => Set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Cidade>> BuscarPorUfAsync(Uf uf, int take = 200, CancellationToken ct = default)
    {
        var list = await Set.AsNoTracking()
                            .Where(x => x.Uf == uf)
                            .OrderBy(x => x.Nome.Nome)                 // Nome é VO: Nome.Nome
                            .Take(Math.Clamp(take, 0, 1000))
                            .ToListAsync(ct);
        return list;
    }

    public async Task<IReadOnlyList<Cidade>> BuscarPorNomeAsync(string nome, int take = 50, CancellationToken ct = default)
    {
        nome = (nome ?? string.Empty).Trim();

        var list = await Set.AsNoTracking()
                            .Where(x => EF.Functions.ILike(x.Nome.Nome, $"%{nome}%"))
                            .OrderBy(x => x.Nome.Nome)
                            .Take(Math.Clamp(take, 1, 200))
                            .ToListAsync(ct);
        return list;
    }

    public async Task<IReadOnlyList<Cidade>> BuscarPorNomeEUfAsync(string nome, Uf uf, int take = 50, CancellationToken ct = default)
    {
        nome = (nome ?? string.Empty).Trim();

        var list = await Set.AsNoTracking()
                            .Where(x => x.Uf == uf && EF.Functions.ILike(x.Nome.Nome, $"%{nome}%"))
                            .OrderBy(x => x.Nome.Nome)
                            .Take(Math.Clamp(take, 1, 200))
                            .ToListAsync(ct);
        return list;
    }
}