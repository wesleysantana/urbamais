using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Suprimentos;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PurchaseConfig
{
    public PurchaseConfig(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Compra>()
            .ToTable("purchase");

        builder.Entity<Compra>()
            .Property(x => x.PedidoId)
            .HasColumnName("order_id")
            .IsRequired();

        builder.Entity<Compra>()
            .Property(x => x.InsumoId)
            .HasColumnName("input_id")
            .IsRequired();

        builder.Entity<Compra>()
            .HasKey(x => new { x.PedidoId, x.InsumoId });

        builder.Entity<Compra>()
            .Property(x => x.FornecedorId)
            .HasColumnName("supplier_id")
            .IsRequired();

        builder.Entity<Compra>()
            .Property(x => x.Quantidade)
            .HasColumnName("amount")
            .IsRequired();

        builder.Entity<Compra>()
            .Property(x => x.ValorUnitario)
            .HasColumnName("unitary_value")
            .IsRequired();

        builder.Entity<Compra>()
            .Property(x => x.DataEntrega)
            .HasColumnName("delivery_date");

        builder.Entity<Compra>()
            .Property(x => x.LocalEntregaId)
            .HasColumnName("delivery_place_id");

        builder.Entity<Compra>()
            .HasOne(x => x.LocalEntregaId)
            .WithMany(x => x.Compras)
            .HasForeignKey(x => x.LocalEntregaId);
    }
}