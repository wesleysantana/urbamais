using Core.Domain.Interfaces;
using Urbamais.Infra.Config;

namespace Urbamais.Infra.Repositories.Shared;
public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly ContextEf _ctx;
    public EfUnitOfWork(ContextEf ctx) => _ctx = ctx;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _ctx.SaveChangesAsync(ct);
}
