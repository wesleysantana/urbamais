using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityUserConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityUser>().ToTable("usuario");
        builder.Entity<IdentityUser>().HasKey(x => x.Id);

        builder.Entity<IdentityUser>()
            .Property(p => p.Id)
            .HasColumnName("id");

        builder.Entity<IdentityUser>()
            .Property(x => x.UserName)
            .HasColumnName("nome")
            .IsRequired();

        builder.Entity<IdentityUser>()
           .Property(x => x.NormalizedUserName)
           .HasColumnName("nome_normalizado");

        builder.Entity<IdentityUser>()
            .Property(p => p.Email)
            .HasColumnName("email")
            .IsRequired();

        builder.Entity<IdentityUser>()
           .Property(x => x.NormalizedEmail)
           .HasColumnName("email_normalizado");

        builder.Entity<IdentityUser>()
            .Property(p => p.EmailConfirmed)
            .HasColumnName("email_confirmado")
            .IsRequired();

        builder.Entity<IdentityUser>()
            .Property(p => p.PasswordHash)
            .HasColumnName("senha")
            .IsRequired();

        builder.Entity<IdentityUser>()
            .Property(p => p.SecurityStamp)
            .HasColumnName("security_stamp");

        builder.Entity<IdentityUser>()
            .Property(p => p.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");

        builder.Entity<IdentityUser>()
            .Property(p => p.PhoneNumber)
            .HasColumnName("telefone")
            .HasMaxLength(20);

        builder.Entity<IdentityUser>()
            .Property(p => p.PhoneNumberConfirmed)
            .HasColumnName("telefone_confirmado")
            .IsRequired();

        builder.Entity<IdentityUser>()
            .Property(p => p.TwoFactorEnabled)
            .HasColumnName("two_factor_habilitado")
            .IsRequired();

        builder.Entity<IdentityUser>()
            .Property(p => p.LockoutEnd)
            .HasColumnName("lockout_end");

        builder.Entity<IdentityUser>()
            .Property(p => p.LockoutEnabled)
            .HasColumnName("lockout_enable")
            .IsRequired();

        builder.Entity<IdentityUser>()
            .Property(p => p.AccessFailedCount)
            .HasColumnName("access_failed_count")
            .IsRequired();
    }
}