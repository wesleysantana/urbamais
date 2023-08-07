using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PlanningInputsConfig
{
    public PlanningInputsConfig(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    { 
        builder.Entity<PlanejamentoInsumo>()
            .ToTable("planning_inputs");

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.PlanejamentoId)
            .HasColumnName("planning_id")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.InsumoId)
            .HasColumnName("input_id")
            .IsRequired();      

        builder.Entity<PlanejamentoInsumo>()
            .HasKey(x => new { x.Id, x.PlanejamentoId, x.InsumoId });

        builder.Entity<PlanejamentoInsumo>()
          .Property(x => x.UnidadeId)
          .HasColumnName("unit_id")
          .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.Quantidade)
            .HasColumnName("amount")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.ValorUnitario)
            .HasColumnName("unitary_value")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.DataInicial)
            .HasColumnName("start_date")
            .IsRequired();

        builder.Entity<PlanejamentoInsumo>()
            .Property(x => x.DataFinal)
            .HasColumnName("final_date")
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