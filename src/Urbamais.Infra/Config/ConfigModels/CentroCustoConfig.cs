using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Financeiro;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CentroCustoConfig : ConfigBase<CentroCusto>
{
    public CentroCustoConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<CentroCusto>().ToTable("centro_custo");

        builder.Entity<CentroCusto>()
            .Property(x => x.Reduzido)
            .HasColumnName("reduzido")
            .IsRequired();

        builder.Entity<CentroCusto>()
            .OwnsOne(x => x.Descricao)
            .Property(x => x.Value)
            .HasColumnName("descricao")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<CentroCusto>()
            .Property(x => x.Natureza)
            .HasColumnName("natureza");

        builder.Entity<CentroCusto>()
            .Property(x => x.Extenso)
            .HasColumnName("extenso");
    }
}