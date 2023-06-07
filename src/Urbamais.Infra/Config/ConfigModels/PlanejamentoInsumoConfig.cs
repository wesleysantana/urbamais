using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Obra;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PlanejamentoInsumoConfig
{
    public PlanejamentoInsumoConfig(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.PlanejamentoId)
            .HasColumnName("planejamneto_id")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.InsumoId)
            .HasColumnName("insumo_id")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .HasKey(x => new { x.PlanejamentoId, x.InsumoId });       

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.Quantidade)
            .HasColumnName("quantidade")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.ValorUnitario)
            .HasColumnName("valor_unitario")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.DataInicio)
            .HasColumnName("data_inicio")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.DataFim)
            .HasColumnName("data_fim")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .HasOne(x => x.Planejamento)
            .WithMany(x => x.PlanejamentosInsumos)
            .HasForeignKey(x => x.PlanejamentoId)
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .HasOne(x => x.Insumo)
            .WithMany(x => x.PlanejamentosInsumos)
            .HasForeignKey(x => x.InsumoId)
            .IsRequired();
    }
}