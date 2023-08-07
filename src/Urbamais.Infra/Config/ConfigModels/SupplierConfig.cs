using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class SupplierConfig : ConfigBase<Fornecedor>
{
    public SupplierConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Fornecedor>()
          .OwnsOne(x => x.NomeFantasia)
          .Property(x => x.Value)
          .HasColumnName("trade_name")
          .IsRequired()
          .HasMaxLength(255);

        builder.Entity<Fornecedor>()
           .OwnsOne(x => x.RazaoSocial)
           .Property(x => x.Value)
           .HasColumnName("corporate_name")
           .IsRequired()
           .HasMaxLength(255);

        builder.Entity<Fornecedor>()
           .OwnsOne(x => x.Cnpj)
           .Property(x => x.Value)
           .HasColumnName("cnpj")
           .IsRequired()
           .HasMaxLength(14);

        builder.Entity<Fornecedor>()
            .Property(x => x.InscricaoEstadual)
            .HasColumnName("state_registration")
            .HasMaxLength(50);

        builder.Entity<Fornecedor>()
           .Property(x => x.IncricaoMunicipal)
           .HasColumnName("municipal_registration")
           .HasMaxLength(50);

        builder.Entity<Fornecedor>()
            .HasMany(x => x.Enderecos)
            .WithMany(x => x.Fornecedores)
            .UsingEntity<Dictionary<string, object>>(
                "suppliers_addresses",
                x => x.HasOne<Endereco>().WithMany().HasForeignKey("address_id"),
                x => x.HasOne<Fornecedor>().WithMany().HasForeignKey("supplier_id")
            );

        builder.Entity<Fornecedor>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Fornecedores)
            .UsingEntity<Dictionary<string, object>>(
                "suppliers_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Fornecedor>().WithMany().HasForeignKey("supplier_id")
            );

        builder.Entity<Fornecedor>()
           .HasMany(x => x.Telefones)
           .WithMany(x => x.Fornecedores)
           .UsingEntity<Dictionary<string, object>>(
               "suppliers_phones",
               x => x.HasOne<Telefone>().WithMany().HasForeignKey("phone_id"),
               x => x.HasOne<Fornecedor>().WithMany().HasForeignKey("supplier_id")
           );
    }
}