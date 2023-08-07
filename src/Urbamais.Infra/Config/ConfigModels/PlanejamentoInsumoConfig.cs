using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PlanejamentoInsumoConfig
{
    public PlanejamentoInsumoConfig(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<PlanejamentoInsumo>().ToTable("planejamentos_insumos");

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.PlanejamentoId)
            .HasColumnName("id_planejamento")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.InsumoId)
            .HasColumnName("id_insumo")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .HasKey(x => new { x.Id, x.PlanejamentoId, x.InsumoId });

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.UnidadeId)
            .HasColumnName("id_unidade")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.Quantidade)
            .HasColumnName("quantidade")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.ValorUnitario)
            .HasColumnName("valor_unitario")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.DataInicial)
            .HasColumnName("data_inicial")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.DataFinal)
            .HasColumnName("data_final")
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