using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Infra.Config;

namespace Urbamais.Infra.Repositories.Planejamento
{
    public sealed class UnidadeRepository : IUnidadeRepository
    {
        private readonly ContextEf _ctx;
        private DbSet<Unidade> Set => _ctx.Set<Unidade>();

        public UnidadeRepository(ContextEf ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        // === Escrita ===
        public Task AddAsync(Unidade entity, CancellationToken ct = default)
            => Set.AddAsync(entity, ct).AsTask();

        public void Update(Unidade entity)
            => Set.Update(entity);

        public void Remove(Unidade entity)
            => Set.Remove(entity); // hard delete; para soft: entity.Delete(); Update(entity);

        // === Leitura ===
        public Task<Unidade?> GetByIdAsync(int id, CancellationToken ct = default)
            => Set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

        public Task<Unidade?> ObterPorSiglaAsync(string sigla, CancellationToken ct = default)
        {
            var s = (sigla ?? string.Empty).Trim().ToUpperInvariant();
            return Set.AsNoTracking().FirstOrDefaultAsync(x => x.Sigla == s, ct);
        }

        public async Task<IReadOnlyList<Unidade>> BuscarPorDescricaoAsync(string termo, int take = 50, CancellationToken ct = default)
        {
            termo = (termo ?? string.Empty).Trim();
            take = Math.Clamp(take, 1, 200);

            var list = await Set.AsNoTracking()
                                .Where(x => x.Descricao != null && EF.Functions.ILike(x.Descricao, $"%{termo}%"))
                                .OrderBy(x => x.Descricao)
                                .Take(take)
                                .ToListAsync(ct);

            return list; // List<Unidade> -> IReadOnlyList<Unidade>
        }

        public Task<bool> ExisteSiglaAsync(string sigla, CancellationToken ct = default)
        {
            var s = (sigla ?? string.Empty).Trim().ToUpperInvariant();
            return Set.AsNoTracking().AnyAsync(x => x.Sigla == s, ct);
        }
    }
}
