using AutoMapper;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Urbamais.CrossCutting.AutoMapper;
using Urbamais.CrossCutting.IOC;
using Urbamais.WebApi.Swagger;

namespace Urbamais.WebApi;

public static class Bootstrap
{
    public static void AddService(IServiceCollection services, IConfiguration configuration)
    {
        Infra.Config.ConfigService.RegisterServices(services, configuration);
        Identity.Config.ConfigService.RegisterServices(services, configuration);
        AuthenticationSetup.AddAuthentication(services, configuration);

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        ModuloIOC.Configure(services);
    }
}