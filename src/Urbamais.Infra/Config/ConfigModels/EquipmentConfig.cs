using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Supplier;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class EquipmentConfig : ConfigBase<Equipment>
{
    public EquipmentConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Equipment>()
            .OwnsOne(x => x.Name)
            .Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Entity<Equipment>()
            .OwnsOne(x => x.Description)
            .Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Entity<Equipment>()
            .HasMany(x => x.Suppliers)
            .WithMany(x => x.Equipments)
            .UsingEntity<Dictionary<string, object>>(
                "suppliers_equipaments",
                x => x.HasOne<Supplier>().WithMany().HasForeignKey("supplier_id"),
                x => x.HasOne<Equipment>().WithMany().HasForeignKey("equipament_id")
            );
    }
}