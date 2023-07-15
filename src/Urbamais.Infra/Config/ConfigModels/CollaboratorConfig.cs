using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Supplier;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CollaboratorConfig : ConfigBase<Collaborator>
{
    public CollaboratorConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Collaborator>()
            .OwnsOne(x => x.Name)
            .Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Collaborator>()
            .OwnsOne(x => x.Cpf)
            .Property(x => x.Cpf)
            .HasColumnName("cpf")
        .IsRequired()
        .HasMaxLength(11);

        builder.Entity<Collaborator>()
            .Property(x => x.NumberCTPS)
            .HasColumnName("number_ctps")
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<Collaborator>()
            .Property(x => x.CTPS)
            .HasColumnName("ctps")
            .IsRequired()
            .HasMaxLength(25);

        builder.Entity<Collaborator>()
           .Property(x => x.NumberCNH)
           .HasColumnName("number_cnh")
           .HasMaxLength(9);

        builder.Entity<Collaborator>()
          .Property(x => x.CNH)
          .HasColumnName("cnh")
          .HasMaxLength(50);

        builder.Entity<Collaborator>()
            .Property(x => x.TypeCNH)
            .HasColumnName("type_cnh")
            .HasMaxLength(2);

        builder.Entity<Collaborator>()
            .Property(x => x.ExpirationDateCNH)
            .HasColumnName("expiration_date_cnh");

        builder.Entity<Collaborator>()
          .Property(x => x.EPI)
          .HasColumnName("epi")
          .HasMaxLength(50);

        builder.Entity<Collaborator>()
          .Property(x => x.AdmissionExam)
          .HasColumnName("admission_exam")
          .HasMaxLength(50);

        builder.Entity<Collaborator>()
          .Property(x => x.RegistrationForm)
          .HasColumnName("registration_form")
          .HasMaxLength(50);

        builder.Entity<Collaborator>()
         .Property(x => x.ServiceOrder)
         .HasColumnName("service_order")
         .HasMaxLength(50);

        builder.Entity<Collaborator>()
            .Property(x => x.NumberAdmissionExam)
            .HasColumnName("number_admission_exam")
            .HasMaxLength(20);

        builder.Entity<Collaborator>()
            .Property(x => x.ExpirationDateAdmissionExam)
            .HasColumnName("expiration_date_admission_exam");

        builder.Entity<Collaborator>()
            .HasMany(x => x.Address)
            .WithMany(x => x.Collaborators)
            .UsingEntity<Dictionary<string, object>>(
                "collaborators_addresses",
                x => x.HasOne<Address>().WithMany().HasForeignKey("address_id"),
                x => x.HasOne<Collaborator>().WithMany().HasForeignKey("collaborator_id")
            );

        builder.Entity<Collaborator>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Collaborators)
            .UsingEntity<Dictionary<string, object>>(
                "collaborators_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Collaborator>().WithMany().HasForeignKey("collaborator_id")
            );

        builder.Entity<Collaborator>()
           .HasMany(x => x.Phones)
           .WithMany(x => x.Collaborators)
           .UsingEntity<Dictionary<string, object>>(
               "collaborators_phones",
               x => x.HasOne<Phone>().WithMany().HasForeignKey("phone_id"),
               x => x.HasOne<Collaborator>().WithMany().HasForeignKey("collaborator_id")
           );
    }
}