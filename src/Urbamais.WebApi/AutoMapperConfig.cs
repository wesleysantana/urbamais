using Autofac;
using AutoMapper;
using Urbamais.CrossCutting.AutoMapper;

namespace Urbamais.WebApi;

public static class AutoMapperConfig
{
    public static void Configure(ContainerBuilder builder)
    {
        // Crie uma instância do MapperConfiguration e registre seus perfis
        var config = new MapperConfiguration(cfg =>
        {
            // Configure seus mapeamentos aqui
            // Exemplo: cfg.CreateMap<SourceClass, DestinationClass>();
            cfg.AddProfile(new MappingProfile());
        });

        // Registre a instância do IMapper no contêiner do Autofac
        builder.RegisterInstance(config.CreateMapper()).As<IMapper>();
    }
}

