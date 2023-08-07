using Microsoft.Extensions.DependencyInjection;
using Urbamais.Application.App.ConcreteClasses.Construction;
using Urbamais.Application.App.ConcreteClasses.Core;
using Urbamais.Application.App.ConcreteClasses.Planning;
using Urbamais.Application.App.Interfaces.Construction;
using Urbamais.Application.App.Interfaces.Core;
using Urbamais.Application.App.Interfaces.Planning;
using Urbamais.Application.Interfaces.Construction;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Application.Interfaces.Generic;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Interfaces.Planning;
using Urbamais.Application.Services.Construction;
using Urbamais.Application.Services.Core;
using Urbamais.Application.Services.Generic;
using Urbamais.Application.Services.Plannig;
using Urbamais.Application.Services.Planning;
using Urbamais.Domain.InterfacesRepositories.Obras;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Domain.InterfacesRepositories.Generic;
using Urbamais.Domain.InterfacesRepositories.Planejamentos;
using Urbamais.Domain.Services.ConcreteClasses.Obras;
using Urbamais.Domain.Services.ConcreteClasses.Core;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.ConcreteClasses.Planejamentos;
using Urbamais.Domain.Services.Interfaces.Obras;
using Urbamais.Domain.Services.Interfaces.Core;
using Urbamais.Domain.Services.Interfaces.Generic;
using Urbamais.Domain.Services.Interfaces.Planejamentos;
using Urbamais.Infra.Repositories.Obras;
using Urbamais.Infra.Repositories.Core;
using Urbamais.Infra.Repositories.Generic;
using Urbamais.Infra.Repositories.Planning;

namespace Urbamais.CrossCutting.IOC;

public static class ModuloIOC
{
    public static void Configure(IServiceCollection services)
    {
        #region App

        services.AddTransient<ICityApp, CityApp>();
        services.AddTransient<ICompanieApp, CompanieApp>();
        services.AddTransient<IInputApp, InputApp>();
        services.AddTransient<IUnitApp, UnitApp>();

        #endregion App

        #region AppServices

        services.AddTransient(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
        services.AddTransient<ICityAppService, CityAppService>();
        services.AddTransient<ICompanieAppService, CompanieAppService>();
        services.AddTransient<IInputAppService, InputAppService>();
        services.AddTransient<IUnitAppService, UnitAppService>();

        #endregion AppServices

        #region Services

        services.AddTransient(typeof(IServiceBase<>), typeof(ServiceBase<>));
        services.AddTransient<ICityService, CidadeService>();
        services.AddTransient<IEmpresaService, EmpresaService>();
        services.AddTransient<IInsumoService, InsumoService>();
        services.AddTransient<IUnidadeService, UnidadeService>();

        #endregion Services

        #region Repositories

        services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddTransient<ICityRepository, CidadeRepository>();
        services.AddTransient<IEmpresaRepository, EmpresaRepository>();
        services.AddTransient<IInsumoRepository, InsumoRepository>();
        services.AddTransient<IUnidadeRepository, UnidadeRepository>();

        #endregion Repositories
    }
}