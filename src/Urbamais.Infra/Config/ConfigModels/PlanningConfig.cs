using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PlanningConfig : ConfigBase<Planejamento>
{
    public PlanningConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Planejamento>()
            .Property(x => x.ObraId)
            .HasColumnName("construction_id")
            .IsRequired();

        builder.Entity<Planejamento>()
            .HasOne(x => x.Obra)
            .WithMany(x => x.Planejamentos)
            .HasForeignKey(x => x.ObraId);
    }
}