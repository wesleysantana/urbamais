using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Suprimentos;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class OrderConfig : ConfigBase<Pedido>
{
    public OrderConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Pedido>()
            .Property(x => x.PlanejamentoId)
            .HasColumnName("planning_id")
            .IsRequired();

        builder.Entity<Pedido>()
            .HasOne(x => x.Planejamento)
            .WithMany(x => x.Ordens)
            .HasForeignKey(x => x.PlanejamentoId)
            .IsRequired();
    }
}