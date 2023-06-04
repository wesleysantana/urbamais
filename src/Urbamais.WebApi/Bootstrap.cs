using AutoMapper;
using Urbamais.Application.Config;

namespace Urbamais.WebApi;

public static class Bootstrap
{
    public static void AddService(IServiceCollection services, IConfiguration configuration)
    {
        Infra.Config.ConfigService.AddServices(services, configuration);
        Identity.Config.ConfigService.AddServices(services, configuration);

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}