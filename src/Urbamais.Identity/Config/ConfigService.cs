using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Urbamais.Identity.Config;

public static class ConfigService
{
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContextIdentity>(
           options => options.UseNpgsql((configuration.GetConnectionString("DefaultConnection")!),
           o => o.SetPostgresVersion(9, 6)), ServiceLifetime.Transient);
    }
}