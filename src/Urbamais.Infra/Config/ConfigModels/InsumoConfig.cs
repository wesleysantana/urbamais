using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class InsumoConfig : ConfigBase<Insumo>
{
    public InsumoConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Insumo>()
            .Property(x => x.Tipo)
            .HasColumnName("tipo")
            .IsRequired();

        builder.Entity<Insumo>()
            .OwnsOne(x => x.Nome)
            .Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Insumo>()
            .Property(x => x.Descricao)
            .HasColumnName("descricao")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Insumo>()
            .Property(x => x.UnidadeId)
            .HasColumnName("unidade_id")
            .IsRequired();

        builder.Entity<Insumo>()
            .HasOne(x => x.Unidade)
            .WithMany(x => x.Insumos)
            .HasForeignKey(x => x.UnidadeId);
    }
}