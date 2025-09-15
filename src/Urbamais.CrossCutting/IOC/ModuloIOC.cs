using Core.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Services.Core;
using Urbamais.Application.Services.Planejamento;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Infra.Repositories.Core;
using Urbamais.Infra.Repositories.Planejamento;
using Urbamais.Infra.Repositories.Shared;

namespace Urbamais.CrossCutting.IOC;

public static class ModuloIOC
{
    public static void Configure(IServiceCollection services)
    {
        // UoW (implementação via DbContext)
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();


        #region Repositories 
        services.AddScoped<ICidadeRepository, CidadeRepository>();
        services.AddScoped<IUnidadeRepository, UnidadeRepository>();
        #endregion

        #region Services
        services.AddScoped<ICidadeAppService, CidadeAppService>();
        services.AddScoped<IUnidadeAppService, UnidadeAppService>();
        #endregion
    }
}