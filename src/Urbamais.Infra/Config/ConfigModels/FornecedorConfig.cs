using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class FornecedorConfig : ConfigBase<Fornecedor>
{
    public FornecedorConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Fornecedor>()
     .OwnsOne(x => x.NomeFantasia)
     .Property(x => x.Value)
     .HasColumnName("nome_fantasia")
     .IsRequired()
     .HasMaxLength(255);

        builder.Entity<Fornecedor>()
            .OwnsOne(x => x.RazaoSocial)
            .Property(x => x.Value)
            .HasColumnName("razao_social")
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
            .HasColumnName("inscricao_estadual")
            .HasMaxLength(50);

        builder.Entity<Fornecedor>()
            .Property(x => x.InscricaoMunicipal)
            .HasColumnName("inscricao_municipal")
            .HasMaxLength(50);

        builder.Entity<Fornecedor>()
            .HasMany(x => x.Enderecos)
            .WithMany(x => x.Fornecedores)
            .UsingEntity<Dictionary<string, object>>(
                "fornecedores_enderecos",
                x => x.HasOne<Endereco>().WithMany().HasForeignKey("endereco_id"),
                x => x.HasOne<Fornecedor>().WithMany().HasForeignKey("fornecedor_id")
            );

        builder.Entity<Fornecedor>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Fornecedores)
            .UsingEntity<Dictionary<string, object>>(
                "fornecedores_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Fornecedor>().WithMany().HasForeignKey("fornecedor_id")
            );

        builder.Entity<Fornecedor>()
            .HasMany(x => x.Telefones)
            .WithMany(x => x.Fornecedores)
            .UsingEntity<Dictionary<string, object>>(
                "fornecedores_telefones",
                x => x.HasOne<Telefone>().WithMany().HasForeignKey("telefone_id"),
                x => x.HasOne<Fornecedor>().WithMany().HasForeignKey("fornecedor_id")
            );
    }
}