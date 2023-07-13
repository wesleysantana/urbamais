using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Supply;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class OrderConfig : ConfigBase<Order>
{
    public OrderConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Order>()
            .Property(x => x.PlanningId)
            .HasColumnName("planning_id")
            .IsRequired();

        builder.Entity<Order>()
            .HasOne(x => x.Planning)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.PlanningId)
            .IsRequired();
    }
}