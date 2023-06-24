using Autofac;
using Urbamais.Application.Services.Core;
using Urbamais.Application.Services.Generic;
using Urbamais.Domain.InterfacesRepositories.Core;
using Urbamais.Domain.InterfacesRepositories.Generic;
using Urbamais.Domain.Services.ConcreteClasses.Core;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Core;
using Urbamais.Domain.Services.Interfaces.Generic;
using Urbamais.Infra.Repositories.Core;
using Urbamais.Infra.Repositories.Generic;
using Urbamais.Application.Interfaces.Generic;
using Urbamais.Application.Interfaces.Core;

namespace Urbamais.CrossCutting.Autofac;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        #region AppServices

        builder.RegisterGeneric(typeof(AppServiceBase<>)).As(typeof(IAppServiceBase<>));
        builder.RegisterType<CidadeAppService>().As<ICidadeAppService>();

        #endregion AppServices

        #region Services

        builder.RegisterGeneric(typeof(ServiceBase<>)).As(typeof(IServiceBase<>));
        builder.RegisterType<CidadeService>().As<ICidadeService>();

        #endregion Services

        #region Repositories

        builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>));
        builder.RegisterType<CidadeRepository>().As<ICidadeRepository>();

        #endregion Repositories
    }
}