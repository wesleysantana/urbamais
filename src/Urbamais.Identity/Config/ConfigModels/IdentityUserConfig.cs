using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityUserConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable("identity_user");
        builder.Entity<ApplicationUser>().HasKey(x => x.Id);

        builder.Entity<ApplicationUser>()
            .Property(p => p.Id)
            .HasColumnName("id")
            .HasMaxLength(100);

        builder.Entity<ApplicationUser>()
            .Property(x => x.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .Property(x => x.IdUserCreation)
            .HasColumnName("id_user_creation")
            .HasMaxLength(100)
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .Property(x => x.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .Property(x => x.IdUserModification)
            .HasColumnName("id_user_modification")
            .HasMaxLength(100);

        builder.Entity<ApplicationUser>()
            .Property(x => x.ModificationDate)
            .HasColumnName("modification_date");

        builder.Entity<ApplicationUser>()
            .Property(x => x.IdUserDeletion)
            .HasColumnName("id_user_deletion")
            .HasMaxLength(100);

        builder.Entity<ApplicationUser>()
            .Property(x => x.DeletionDate)
            .HasColumnName("deletion_date");

        builder.Entity<ApplicationUser>()
           .Property(x => x.NormalizedUserName)
           .HasColumnName("normalized_user_name")
           .HasMaxLength(100);

        builder.Entity<ApplicationUser>()
            .Property(p => p.Email)
            .HasColumnName("email")
            .HasMaxLength(100)
            .IsRequired();

        builder.Entity<ApplicationUser>()
           .Property(x => x.NormalizedEmail)
           .HasColumnName("normalized_email")
           .HasMaxLength(100);

        builder.Entity<ApplicationUser>()
            .Property(p => p.EmailConfirmed)
            .HasColumnName("email_confirmed")
            .HasMaxLength(100)
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .Property(p => p.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(100)
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .Property(p => p.SecurityStamp)
            .HasColumnName("security_stamp")
            .HasMaxLength(100);

        builder.Entity<ApplicationUser>()
            .Property(p => p.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp")
            .HasMaxLength(100);

        builder.Entity<ApplicationUser>()
            .Property(p => p.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(20);

        builder.Entity<ApplicationUser>()
            .Property(p => p.PhoneNumberConfirmed)
            .HasColumnName("phone_number_confirmed")
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .Property(p => p.TwoFactorEnabled)
            .HasColumnName("two_factor_enabled")
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .Property(p => p.LockoutEnd)
            .HasColumnName("lockout_end");

        builder.Entity<ApplicationUser>()
            .Property(p => p.LockoutEnabled)
            .HasColumnName("lockout_enable")
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .Property(p => p.AccessFailedCount)
            .HasColumnName("access_failed_count")
            .IsRequired();

        // Criar Usuário e Perfil e vincular o usuário criado ao perfil
        // Verifica se a role 'developer' existe
        var developerRoleId = "af92719d-1d7f-4c80-aadc-ead1e2ab3a9d"; // Define um ID para a role 'developer'

        var developerRoleExists = builder.Entity<IdentityRole>()
            .HasData(new IdentityRole
            {
                Id = developerRoleId,
                Name = "developer",
                NormalizedName = "DEVELOPER"
            });

        // Criação de usuário
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        var user = new ApplicationUser
        {
            UserName = "dev@metamais.com",
            NormalizedUserName = "DEV@METAMAIS.COM",
            Email = "dev@metamais.com",
            NormalizedEmail = "DEV@METAMAIS.COM",
            EmailConfirmed = true,
            IdUserCreation = Guid.NewGuid().ToString()
        };
        user.PasswordHash = passwordHasher.HashPassword(user, "123123");
        builder.Entity<ApplicationUser>().HasData(user);

        // Vincula o usuário à role 'developer'
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            UserId = user.Id,
            RoleId = developerRoleId
        });
    }
}