using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class UnidadeConfig : ConfigBase<Unidade>
{
    public UnidadeConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Unidade>()
            .Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(50)
            .IsRequired();
    }
}