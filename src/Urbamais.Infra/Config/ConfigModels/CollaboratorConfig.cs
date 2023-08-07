using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CollaboratorConfig : ConfigBase<Colaborador>
{
    public CollaboratorConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Colaborador>()
            .OwnsOne(x => x.Nome)
            .Property(x => x.Value)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Colaborador>()
            .OwnsOne(x => x.Cpf)
            .Property(x => x.Value)
            .HasColumnName("cpf")
        .IsRequired()
        .HasMaxLength(11);

        builder.Entity<Colaborador>()
            .Property(x => x.NumeroCTPS)
            .HasColumnName("number_ctps")
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<Colaborador>()
            .Property(x => x.CTPS)
            .HasColumnName("ctps")
            .IsRequired()
            .HasMaxLength(25);

        builder.Entity<Colaborador>()
           .Property(x => x.NumeroCNH)
           .HasColumnName("number_cnh")
           .HasMaxLength(9);

        builder.Entity<Colaborador>()
          .Property(x => x.CNH)
          .HasColumnName("cnh")
          .HasMaxLength(50);

        builder.Entity<Colaborador>()
            .Property(x => x.TipoCNH)
            .HasColumnName("type_cnh")
            .HasMaxLength(2);

        builder.Entity<Colaborador>()
            .Property(x => x.DataExpiracaoCNH)
            .HasColumnName("expiration_date_cnh");

        builder.Entity<Colaborador>()
          .Property(x => x.EPI)
          .HasColumnName("epi")
          .HasMaxLength(50);

        builder.Entity<Colaborador>()
          .Property(x => x.ExameAdimissional)
          .HasColumnName("admission_exam")
          .HasMaxLength(50);

        builder.Entity<Colaborador>()
          .Property(x => x.FichaRegistro)
          .HasColumnName("registration_form")
          .HasMaxLength(50);

        builder.Entity<Colaborador>()
         .Property(x => x.OrdemServico)
         .HasColumnName("service_order")
         .HasMaxLength(50);

        builder.Entity<Colaborador>()
            .Property(x => x.NumeroExameAdmissional)
            .HasColumnName("number_admission_exam")
            .HasMaxLength(20);

        builder.Entity<Colaborador>()
            .Property(x => x.DataExpiracaoExameAdmissional)
            .HasColumnName("expiration_date_admission_exam");

        builder.Entity<Colaborador>()
            .HasMany(x => x.Enderecos)
            .WithMany(x => x.Colaboradores)
            .UsingEntity<Dictionary<string, object>>(
                "collaborators_addresses",
                x => x.HasOne<Endereco>().WithMany().HasForeignKey("address_id"),
                x => x.HasOne<Colaborador>().WithMany().HasForeignKey("collaborator_id")
            );

        builder.Entity<Colaborador>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Colaboradores)
            .UsingEntity<Dictionary<string, object>>(
                "collaborators_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Colaborador>().WithMany().HasForeignKey("collaborator_id")
            );

        builder.Entity<Colaborador>()
           .HasMany(x => x.Telefones)
           .WithMany(x => x.Colaboradores)
           .UsingEntity<Dictionary<string, object>>(
               "collaborators_phones",
               x => x.HasOne<Telefone>().WithMany().HasForeignKey("phone_id"),
               x => x.HasOne<Colaborador>().WithMany().HasForeignKey("collaborator_id")
           );
    }
}