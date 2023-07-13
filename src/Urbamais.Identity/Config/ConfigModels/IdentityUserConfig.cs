using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityUserConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityUser>().ToTable("identity_user");
        builder.Entity<IdentityUser>().HasKey(x => x.Id);

        builder.Entity<IdentityUser>()
            .Property(p => p.Id)
            .HasColumnName("id");

        builder.Entity<IdentityUser>()
            .Property(x => x.UserName)
            .HasColumnName("user_name")
            .IsRequired();

        builder.Entity<IdentityUser>()
           .Property(x => x.NormalizedUserName)
           .HasColumnName("normalized_user_name");

        builder.Entity<IdentityUser>()
            .Property(p => p.Email)
            .HasColumnName("email")
            .IsRequired();

        builder.Entity<IdentityUser>()
           .Property(x => x.NormalizedEmail)
           .HasColumnName("normalized_email");

        builder.Entity<IdentityUser>()
            .Property(p => p.EmailConfirmed)
            .HasColumnName("email_confirmed")
            .IsRequired();

        builder.Entity<IdentityUser>()
            .Property(p => p.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Entity<IdentityUser>()
            .Property(p => p.SecurityStamp)
            .HasColumnName("security_stamp");

        builder.Entity<IdentityUser>()
            .Property(p => p.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");

        builder.Entity<IdentityUser>()
            .Property(p => p.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(20);

        builder.Entity<IdentityUser>()
            .Property(p => p.PhoneNumberConfirmed)
            .HasColumnName("phone_number_confirmed")
            .IsRequired();

        builder.Entity<IdentityUser>()
            .Property(p => p.TwoFactorEnabled)
            .HasColumnName("two_factor_enabled")
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