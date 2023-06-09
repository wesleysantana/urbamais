using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Suprimento;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PedidosConfig : ConfigBase<Pedido>
{
    public PedidosConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Pedido>()
            .Property(x => x.PlanejamentoId)
            .HasColumnName("planejamento_id")
            .IsRequired();

        builder.Entity<Pedido>()
            .HasOne(x => x.Planejamento)
            .WithMany(x => x.Pedidos)
            .HasForeignKey(x => x.PlanejamentoId)            
            .IsRequired();
    }
}