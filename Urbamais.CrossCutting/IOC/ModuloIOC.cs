using Microsoft.Extensions.DependencyInjection;
using Urbamais.Application.App.ConcreteClasses.Planejamento;
using Urbamais.Application.App.Interfaces.Planejamento;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Application.Interfaces.Generic;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Core;
using Urbamais.Application.Services.Generic;
using Urbamais.Application.Services.Planejamento;
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

namespace Urbamais.CrossCutting.IOC;

public static class ModuloIOC
{
    public static void Configure(IServiceCollection services)
    {
        #region App

        services.AddTransient<IUnidadeApp, UnidadeApp>();

        #endregion App

        #region AppServices

        services.AddTransient(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
        services.AddTransient<ICidadeAppService, CidadeAppService>();
        services.AddTransient<IUnidadeAppService, UnidadeAppService>();

        #endregion AppServices

        #region Services

        services.AddTransient(typeof(IServiceBase<>), typeof(ServiceBase<>));
        services.AddTransient<ICidadeService, CidadeService>();
        services.AddTransient<IUnidadeService, UnidadeService>();

        #endregion Services

        #region Repositories

        services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddTransient<ICidadeRepository, CidadeRepository>();
        services.AddTransient<IUnidadeRepository, UnidadeRepository>();

        #endregion Repositories

    }
}