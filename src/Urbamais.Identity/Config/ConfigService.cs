using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Urbamais.Application.Interfaces.Identity;
using Urbamais.Identity.Services;

namespace Urbamais.Identity.Config;

public static class ConfigService
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContextIdentity>(
           options => options.UseNpgsql((configuration.GetConnectionString("DefaultConnection")!),
           o => o.SetPostgresVersion(9, 6)), ServiceLifetime.Transient);

        services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ContextIdentity>()
            .AddDefaultTokenProviders();
       
        services.AddScoped<IIdentityAppService, IdentityService>();  
    }
}