using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planning;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PlanningConfig : ConfigBase<Planning>
{
    public PlanningConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Planning>()
            .Property(x => x.ConstructionId)
            .HasColumnName("construction_id")
            .IsRequired();

        builder.Entity<Planning>()
            .HasOne(x => x.Obra)
            .WithMany(x => x.Plannings)
            .HasForeignKey(x => x.ConstructionId);
    }
}