using Microsoft.EntityFrameworkCore;
using Urbamais.Infra.Config;

namespace Urbamais.Infra.Repositories.Shared;
internal abstract class EfRepository<T> where T : class
{
    protected readonly ContextEf Ctx;
    protected DbSet<T> Set => Ctx.Set<T>();
    protected EfRepository(ContextEf ctx) => Ctx = ctx;
}
