using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Construction;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CompanieConfig : ConfigBase<Companie>
{
    public CompanieConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Companie>()
           .OwnsOne(x => x.TradeName)
           .Property(x => x.Value)
           .HasColumnName("trade_name")
           .IsRequired()
           .HasMaxLength(255);

        builder.Entity<Companie>()
           .OwnsOne(x => x.CorporateName)
           .Property(x => x.Value)
           .HasColumnName("corporate_name")
           .IsRequired()
           .HasMaxLength(255);

        builder.Entity<Companie>()
           .OwnsOne(x => x.Cnpj)
           .Property(x => x.Value)
           .HasColumnName("cnpj")
           .IsRequired()
           .HasMaxLength(14);

        builder.Entity<Companie>()
            .Property(x => x.StateRegistration)
            .HasColumnName("state_registration")
            .HasMaxLength(50);

        builder.Entity<Companie>()
           .Property(x => x.MunicipalRegistration)
           .HasColumnName("municipal_registration")
           .HasMaxLength(50);

        builder.Entity<Companie>()
            .HasMany(x => x.Addresses)
            .WithMany(x => x.Companies)
            .UsingEntity<Dictionary<string, object>>(
                "companies_addresses",
                x => x.HasOne<Address>().WithMany().HasForeignKey("address_id"),
                x => x.HasOne<Companie>().WithMany().HasForeignKey("companie_id")
            );

        builder.Entity<Companie>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Companies)
            .UsingEntity<Dictionary<string, object>>(
                "companies_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Companie>().WithMany().HasForeignKey("companie_id")
            );

        builder.Entity<Companie>()
           .HasMany(x => x.Phones)
           .WithMany(x => x.Companies)
           .UsingEntity<Dictionary<string, object>>(
               "companies_phones",
               x => x.HasOne<Phone>().WithMany().HasForeignKey("phone_id"),
               x => x.HasOne<Companie>().WithMany().HasForeignKey("companie_id")
           );
    }
}