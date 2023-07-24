using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PlanningInputsConfig
{
    public PlanningInputsConfig(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    { 
        builder.Entity<PlanningInput>()
            .ToTable("planning_inputs");

        builder.Entity<PlanningInput>()
            .Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Entity<PlanningInput>()
            .Property(x => x.PlanningId)
            .HasColumnName("planning_id")
            .IsRequired();

        builder.Entity<PlanningInput>()
            .Property(x => x.InputId)
            .HasColumnName("input_id")
            .IsRequired();      

        builder.Entity<PlanningInput>()
            .HasKey(x => new { x.Id, x.PlanningId, x.InputId });

        builder.Entity<PlanningInput>()
          .Property(x => x.UnitId)
          .HasColumnName("unit_id")
          .IsRequired();

        builder.Entity<PlanningInput>()
            .Property(x => x.Amount)
            .HasColumnName("amount")
            .IsRequired();

        builder.Entity<PlanningInput>()
            .Property(x => x.UnitaryValue)
            .HasColumnName("unitary_value")
            .IsRequired();

        builder.Entity<PlanningInput>()
            .Property(x => x.StartDate)
            .HasColumnName("start_date")
            .IsRequired();

        builder.Entity<PlanningInput>()
            .Property(x => x.FinalDate)
            .HasColumnName("final_date")
            .IsRequired();

        builder.Entity<PlanningInput>()
            .HasOne(x => x.Planning)
            .WithMany(x => x.PlannigInputs)
            .HasForeignKey(x => x.PlanningId)
            .IsRequired();

        builder.Entity<PlanningInput>()
            .HasOne(x => x.Input)
            .WithMany(x => x.PlannigInputs)
            .HasForeignKey(x => x.InputId)
            .IsRequired();
    }
}