using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.CoreRelationManyToMany;
using Urbamais.Domain.Entities.Fornecedor;
using Urbamais.Domain.Entities.Obra;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class EmpresaConfig : ConfigBase<Empresa>
{
    public EmpresaConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Empresa>()
           .OwnsOne(x => x.NomeFantasia)
           .Property(x => x.Nome)
           .HasColumnName("nome_fantasia")
           .IsRequired()
           .HasMaxLength(255);

        builder.Entity<Empresa>()
           .OwnsOne(x => x.RazaoSocial)
           .Property(x => x.Nome)
           .HasColumnName("razao_social")
           .IsRequired()
           .HasMaxLength(255);       

        builder.Entity<Empresa>()
           .OwnsOne(x => x.Cnpj)
           .Property(x => x.Cnpj)
           .HasColumnName("cnpj")
           .IsRequired()
           .HasMaxLength(14);

        builder.Entity<Empresa>()
            .Property(x => x.InscricaoEstadual)
            .HasColumnName("inscricao_estadual")
            .HasMaxLength(50);

        builder.Entity<Empresa>()
           .Property(x => x.InscricaoMunicipal)
           .HasColumnName("inscricao_municipal")
           .HasMaxLength(50);

        builder.Entity<Empresa>()
            .HasMany(x => x.Enderecos)
            .WithMany(x => x.Empresas)
            .UsingEntity<Dictionary<string, object>>(
                "empresas_enderecos",
                x => x.HasOne<Endereco>().WithMany().HasForeignKey("endereco_id"),
                x => x.HasOne<Empresa>().WithMany().HasForeignKey("empresa_id")
            );

        builder.Entity<Empresa>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Empresas)
            .UsingEntity<Dictionary<string, object>>(
                "empresas_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Empresa>().WithMany().HasForeignKey("empresa_id")
            );

        builder.Entity<Empresa>()
           .HasMany(x => x.Telefones)
           .WithMany(x => x.Empresas)
           .UsingEntity<Dictionary<string, object>>(
               "empresas_telefones",
               x => x.HasOne<Telefone>().WithMany().HasForeignKey("telefone_id"),
               x => x.HasOne<Empresa>().WithMany().HasForeignKey("empresa_id")
           );
    }
}