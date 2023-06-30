using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Fornecedor;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class ColaboradorConfig : ConfigBase<Colaborador>
{
    public ColaboradorConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Colaborador>()
            .OwnsOne(x => x.Nome)
            .Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Colaborador>()
            .OwnsOne(x => x.Cpf)
            .Property(x => x.Cpf)
            .HasColumnName("cpf")
        .IsRequired()
        .HasMaxLength(11);        

        builder.Entity<Colaborador>()
            .Property(x => x.NumeroCarteiraTrabalho)
            .HasColumnName("numero_carteira_trabalho")
            .IsRequired()
            .HasMaxLength(25);

        builder.Entity<Colaborador>()
           .Property(x => x.NumeroCNH)
           .HasColumnName("numero_cnh")
           .HasMaxLength(9);

        builder.Entity<Colaborador>()
            .Property(x => x.TipoCNH)
            .HasColumnName("tipo_cnh")
            .HasMaxLength(2);

        builder.Entity<Colaborador>()
            .Property(x => x.DataValidadeCNH)
            .HasColumnName("data_validade_cnh");

        builder.Entity<Colaborador>()
            .Property(x => x.NumeroExameAdmissional)
            .HasColumnName("numero_exame_admissional")
            .HasMaxLength(20);

        builder.Entity<Colaborador>()
            .Property(x => x.DataValidadeExameAdmissional)
            .HasColumnName("data_validade_exame_admissional");

        builder.Entity<Colaborador>()
            .HasMany(x => x.Enderecos)
            .WithMany(x => x.Colaboradores)
            .UsingEntity<Dictionary<string, object>>(
                "colaboradores_enderecos",
                x => x.HasOne<Endereco>().WithMany().HasForeignKey("endereco_id"),
                x => x.HasOne<Colaborador>().WithMany().HasForeignKey("colaborador_id")
            );

        builder.Entity<Colaborador>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Colaboradores)
            .UsingEntity<Dictionary<string, object>>(
                "colaboradores_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Colaborador>().WithMany().HasForeignKey("colaborador_id")
            );

        builder.Entity<Colaborador>()
           .HasMany(x => x.Telefones)
           .WithMany(x => x.Colaboradores)
           .UsingEntity<Dictionary<string, object>>(
               "colaboradores_telefones",
               x => x.HasOne<Telefone>().WithMany().HasForeignKey("telefone_id"),
               x => x.HasOne<Colaborador>().WithMany().HasForeignKey("colaborador_id")
           );
    }
}