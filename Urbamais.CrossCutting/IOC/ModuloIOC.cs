using Microsoft.Extensions.DependencyInjection;
using Urbamais.Application.App.ConcreteClasses.Planning;
using Urbamais.Application.App.Interfaces.Planning;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Application.Interfaces.Generic;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Interfaces.Planning;
using Urbamais.Application.Services.Core;
using Urbamais.Application.Services.Generic;
using Urbamais.Application.Services.Plannig;
using Urbamais.Application.Services.Planning;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Domain.InterfacesRepositories.Generic;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Domain.Services.ConcreteClasses.Core;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.ConcreteClasses.Planejamento;
using Urbamais.Domain.Services.Interfaces.Core;
using Urbamais.Domain.Services.Interfaces.Generic;
using Urbamais.Domain.Services.Interfaces.Planning;
using Urbamais.Infra.Repositories.Core;
using Urbamais.Infra.Repositories.Generic;
using Urbamais.Infra.Repositories.Planning;

namespace Urbamais.CrossCutting.IOC;

public static class ModuloIOC
{
    public static void Configure(IServiceCollection services)
    {
        #region App

        services.AddTransient<IInputApp, InputApp>();
        services.AddTransient<IUnitApp, UnitApp>();

        #endregion App

        #region AppServices

        services.AddTransient(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
        services.AddTransient<ICityAppService, CityAppService>();
        services.AddTransient<IInputAppService, InputAppService>();
        services.AddTransient<IUnitAppService, UnitAppService>();

        #endregion AppServices

        #region Services

        services.AddTransient(typeof(IServiceBase<>), typeof(ServiceBase<>));
        services.AddTransient<ICityService, CityService>();
        services.AddTransient<IInputService, InputService>();
        services.AddTransient<IUnitService, UnitService>();

        #endregion Services

        #region Repositories

        services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddTransient<ICityRepository, CityRepository>();
        services.AddTransient<IInputRepository, InputRepository>();
        services.AddTransient<IUnitRepository, UnitRepository>();

        #endregion Repositories
    }
}