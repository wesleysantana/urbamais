using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Urbamais.Identity.Config;
using Urbamais.Infra.Config;

namespace Urbamais.Application.Bootstrap;

public static class ConfigService
{
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContextEf>(
           options => options.UseNpgsql((configuration.GetConnectionString("DefaultConnection")!),
           o => o.SetPostgresVersion(9, 6)), ServiceLifetime.Transient);

        services.AddDbContext<ContextIdentity>(
           options => options.UseNpgsql((configuration.GetConnectionString("DefaultConnection")!),
           o => o.SetPostgresVersion(9, 6)), ServiceLifetime.Transient);

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        //services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        //services.AddTransient<IServidorRepository, ServidorRepository>();
        //services.AddTransient<IVideoRepository, VideoRepository>();

        //services.AddTransient<ServidorService>();
        //services.AddTransient<VideoService>();
    }
}