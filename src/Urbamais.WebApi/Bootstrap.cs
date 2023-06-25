using AutoMapper;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Application.Interfaces.Generic;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Core;
using Urbamais.Application.Services.Generic;
using Urbamais.Application.Services.Planejamento;
using Urbamais.CrossCutting.AutoMapper;
using Urbamais.CrossCutting.IOC;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Domain.InterfacesRepositories.Generic;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Domain.Services.ConcreteClasses.Core;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.ConcreteClasses.Planejamento;
using Urbamais.Domain.Services.Interfaces.Core;
using Urbamais.Domain.Services.Interfaces.Generic;
using Urbamais.Domain.Services.Interfaces.Planejamento;
using Urbamais.Infra.Repositories.Core;
using Urbamais.Infra.Repositories.Generic;
using Urbamais.Infra.Repositories.Planejamento;

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

        ModuloIOC.Configure(services);
    }
}