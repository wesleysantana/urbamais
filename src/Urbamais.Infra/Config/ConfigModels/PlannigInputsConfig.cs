using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PlannigInputsConfig
{
    public PlannigInputsConfig(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<PlannigInput>()
            .ToTable("planejamento_insumo");

        builder.Entity<PlannigInput>()
            .Property(x => x.PlannigId)
            .HasColumnName("plannig_id")
            .IsRequired();

        builder.Entity<PlannigInput>()
            .Property(x => x.InputId)
            .HasColumnName("input_id")
            .IsRequired();

        builder.Entity<PlannigInput>()
            .HasKey(x => new { x.PlannigId, x.InputId });

        builder.Entity<PlannigInput>()
            .Property(x => x.Amount)
            .HasColumnName("amount")
            .IsRequired();

        builder.Entity<PlannigInput>()
            .Property(x => x.UnitaryValue)
            .HasColumnName("unitary_value")
            .IsRequired();

        builder.Entity<PlannigInput>()
            .Property(x => x.StartDate)
            .HasColumnName("start_date")
            .IsRequired();

        builder.Entity<PlannigInput>()
            .Property(x => x.FinalDate)
            .HasColumnName("final_date")
            .IsRequired();

        builder.Entity<PlannigInput>()
            .HasOne(x => x.Planning)
            .WithMany(x => x.PlannigInputs)
            .HasForeignKey(x => x.PlannigId)
            .IsRequired();

        builder.Entity<PlannigInput>()
            .HasOne(x => x.Input)
            .WithMany(x => x.PlannigInputs)
            .HasForeignKey(x => x.InputId)
            .IsRequired();
    }
}