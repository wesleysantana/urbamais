using Microsoft.Extensions.DependencyInjection;
using Urbamais.Application.App.ConcreteClasses.Core;
using Urbamais.Application.App.ConcreteClasses.Financeiro;
using Urbamais.Application.App.ConcreteClasses.Obras;
using Urbamais.Application.App.ConcreteClasses.Planejamentos;
using Urbamais.Application.App.Interfaces.Core;
using Urbamais.Application.App.Interfaces.Financeiro;
using Urbamais.Application.App.Interfaces.Obras;
using Urbamais.Application.App.Interfaces.Planejamentos;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Application.Interfaces.Financeiro;
using Urbamais.Application.Interfaces.Generic;
using Urbamais.Application.Interfaces.Obras;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Interfaces.Planning;
using Urbamais.Application.Services.Construction;
using Urbamais.Application.Services.Core;
using Urbamais.Application.Services.Financeiro;
using Urbamais.Application.Services.Generic;
using Urbamais.Application.Services.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Domain.InterfacesRepositories.Financeiro;
using Urbamais.Domain.InterfacesRepositories.Generic;
using Urbamais.Domain.InterfacesRepositories.Obras;
using Urbamais.Domain.InterfacesRepositories.Planejamentos;
using Urbamais.Domain.Services.ConcreteClasses.Core;
using Urbamais.Domain.Services.ConcreteClasses.Financeiro;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.ConcreteClasses.Obras;
using Urbamais.Domain.Services.ConcreteClasses.Planejamentos;
using Urbamais.Domain.Services.Interfaces.Core;
using Urbamais.Domain.Services.Interfaces.Financeiro;
using Urbamais.Domain.Services.Interfaces.Generic;
using Urbamais.Domain.Services.Interfaces.Obras;
using Urbamais.Domain.Services.Interfaces.Planejamentos;
using Urbamais.Infra.Repositories.Core;
using Urbamais.Infra.Repositories.Financeiro;
using Urbamais.Infra.Repositories.Generic;
using Urbamais.Infra.Repositories.Obras;
using Urbamais.Infra.Repositories.Planning;

namespace Urbamais.CrossCutting.IOC;

public static class ModuloIOC
{
    public static void Configure(IServiceCollection services)
    {
        #region App

        services.AddTransient<ICentroCustoApp, CentroCustoApp>();
        services.AddTransient<ICidadeApp, CidadeApp>();
        services.AddTransient<IEmpresaApp, EmpresaApp>();
        services.AddTransient<IInsumoApp, InsumoApp>();
        services.AddTransient<IRegistroFinanceiroApp, RegistroFinanceiroApp>();
        services.AddTransient<IUnidadeApp, UnidadeApp>();

        #endregion App

        #region AppServices
        
        services.AddTransient(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
        services.AddTransient<ICentroCustoAppService, CentroCustoAppService>();
        services.AddTransient<ICidadeAppService, CityAppService>();
        services.AddTransient<IEmpresaAppService, EmpresaAppService>();
        services.AddTransient<IInsumoAppService, InsumoAppService>();
        services.AddTransient<IRegistroFinanceiroAppService, RegistroFinanceiroAppService>();
        services.AddTransient<IUnidadeAppService, UnidadeAppService>();

        #endregion AppServices

        #region Services

        services.AddTransient(typeof(IServiceBase<>), typeof(ServiceBase<>));
        services.AddTransient<ICentroCustoService, CentroCustoService>();
        services.AddTransient<ICityService, CidadeService>();
        services.AddTransient<IEmpresaService, EmpresaService>();
        services.AddTransient<IInsumoService, InsumoService>();
        services.AddTransient<IRegistroFinanceiroService, RegistroFinanceiroService>();
        services.AddTransient<IUnidadeService, UnidadeService>();

        #endregion Services

        #region Repositories

        services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddTransient<ICentroCustoRepository, CentroCustoRepository>();
        services.AddTransient<ICityRepository, CidadeRepository>();
        services.AddTransient<IEmpresaRepository, EmpresaRepository>();
        services.AddTransient<IInsumoRepository, InsumoRepository>();
        services.AddTransient<IRegistroFinanceiroRepository, RegistroFinanceiroRepository>();
        services.AddTransient<IUnidadeRepository, UnidadeRepository>();

        #endregion Repositories
    }
}