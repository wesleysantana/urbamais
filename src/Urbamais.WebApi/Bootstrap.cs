using AutoMapper;
using Urbamais.Application.Config;

namespace Urbamais.WebApi;

public static class Bootstrap
{
    public static void AddService(IServiceCollection services, IConfiguration configuration)
    {
        Infra.Config.ConfigService.RegisterServices(services, configuration);
        Identity.Config.ConfigService.RegisterServices(services, configuration);
        AuthenticationSetup.AddAuthentication(services, configuration);

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}