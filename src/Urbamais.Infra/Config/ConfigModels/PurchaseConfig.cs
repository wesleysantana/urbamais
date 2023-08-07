using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Supply;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PurchaseConfig
{
    public PurchaseConfig(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Purchase>()
            .ToTable("purchase");

        builder.Entity<Purchase>()
            .Property(x => x.OrderId)
            .HasColumnName("order_id")
            .IsRequired();

        builder.Entity<Purchase>()
            .Property(x => x.InputId)
            .HasColumnName("input_id")
            .IsRequired();

        builder.Entity<Purchase>()
            .HasKey(x => new { x.OrderId, x.InputId });

        builder.Entity<Purchase>()
            .Property(x => x.SupplierId)
            .HasColumnName("supplier_id")
            .IsRequired();

        builder.Entity<Purchase>()
            .Property(x => x.Amount)
            .HasColumnName("amount")
            .IsRequired();

        builder.Entity<Purchase>()
            .Property(x => x.UnitaryValue)
            .HasColumnName("unitary_value")
            .IsRequired();

        builder.Entity<Purchase>()
            .Property(x => x.DeliveryDate)
            .HasColumnName("delivery_date");

        builder.Entity<Purchase>()
            .Property(x => x.DeliveryPlaceId)
            .HasColumnName("delivery_place_id");

        builder.Entity<Purchase>()
            .HasOne(x => x.DeliveryPlace)
            .WithMany(x => x.Compras)
            .HasForeignKey(x => x.DeliveryPlaceId);
    }
}