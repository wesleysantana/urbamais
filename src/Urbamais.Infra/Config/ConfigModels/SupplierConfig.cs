using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Supplier;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class SupplierConfig : ConfigBase<Supplier>
{
    public SupplierConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Supplier>()
          .OwnsOne(x => x.TradeName)
          .Property(x => x.Name)
          .HasColumnName("trade_name")
          .IsRequired()
          .HasMaxLength(255);

        builder.Entity<Supplier>()
           .OwnsOne(x => x.CorporateName)
           .Property(x => x.Name)
           .HasColumnName("corporate_name")
           .IsRequired()
           .HasMaxLength(255);

        builder.Entity<Supplier>()
           .OwnsOne(x => x.Cnpj)
           .Property(x => x.Cnpj)
           .HasColumnName("cnpj")
           .IsRequired()
           .HasMaxLength(14);

        builder.Entity<Supplier>()
            .Property(x => x.StateRegistration)
            .HasColumnName("state_registration")
            .HasMaxLength(50);

        builder.Entity<Supplier>()
           .Property(x => x.MunicipalRegistration)
           .HasColumnName("municipal_registration")
           .HasMaxLength(50);

        builder.Entity<Supplier>()
            .HasMany(x => x.Addresses)
            .WithMany(x => x.Supplier)
            .UsingEntity<Dictionary<string, object>>(
                "supplier_addresses",
                x => x.HasOne<Address>().WithMany().HasForeignKey("address_id"),
                x => x.HasOne<Supplier>().WithMany().HasForeignKey("supplier_id")
            );

        builder.Entity<Supplier>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Suppliers)
            .UsingEntity<Dictionary<string, object>>(
                "suppliers_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Supplier>().WithMany().HasForeignKey("supplier_id")
            );

        builder.Entity<Supplier>()
           .HasMany(x => x.Phones)
           .WithMany(x => x.Suppliers)
           .UsingEntity<Dictionary<string, object>>(
               "supplier",
               x => x.HasOne<Phone>().WithMany().HasForeignKey("phone_id"),
               x => x.HasOne<Supplier>().WithMany().HasForeignKey("supplier_id")
           );
    }
}