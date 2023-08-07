using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CompanieConfig : ConfigBase<Empresa>
{
    public CompanieConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Empresa>()
           .OwnsOne(x => x.NomeFantasia)
           .Property(x => x.Value)
           .HasColumnName("trade_name")
           .IsRequired()
           .HasMaxLength(255);

        builder.Entity<Empresa>()
           .OwnsOne(x => x.RazaoSocial)
           .Property(x => x.Value)
           .HasColumnName("corporate_name")
           .IsRequired()
           .HasMaxLength(255);

        builder.Entity<Empresa>()
           .OwnsOne(x => x.Cnpj)
           .Property(x => x.Value)
           .HasColumnName("cnpj")
           .IsRequired()
           .HasMaxLength(14);

        builder.Entity<Empresa>()
            .Property(x => x.InscricaoEstadual)
            .HasColumnName("state_registration")
            .HasMaxLength(50);

        builder.Entity<Empresa>()
           .Property(x => x.IncricaoMunicipal)
           .HasColumnName("municipal_registration")
           .HasMaxLength(50);

        builder.Entity<Empresa>()
            .HasMany(x => x.Enderecos)
            .WithMany(x => x.Empresas)
            .UsingEntity<Dictionary<string, object>>(
                "companies_addresses",
                x => x.HasOne<Endereco>().WithMany().HasForeignKey("address_id"),
                x => x.HasOne<Empresa>().WithMany().HasForeignKey("companie_id")
            );

        builder.Entity<Empresa>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Empresas)
            .UsingEntity<Dictionary<string, object>>(
                "companies_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Empresa>().WithMany().HasForeignKey("companie_id")
            );

        builder.Entity<Empresa>()
           .HasMany(x => x.Telefones)
           .WithMany(x => x.Empresas)
           .UsingEntity<Dictionary<string, object>>(
               "companies_phones",
               x => x.HasOne<Telefone>().WithMany().HasForeignKey("phone_id"),
               x => x.HasOne<Empresa>().WithMany().HasForeignKey("companie_id")
           );
    }
}