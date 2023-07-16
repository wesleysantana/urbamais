using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityUserTokenConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityUserToken<string>>().ToTable("identity_user_token");

        builder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

        builder.Entity<IdentityUserToken<string>>()
            .Property(p => p.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(100);

        builder.Entity<IdentityUserToken<string>>()
            .Property(p => p.LoginProvider)
            .HasColumnName("login_provider")
            .HasMaxLength(100);

        builder.Entity<IdentityUserToken<string>>()
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(100);

        builder.Entity<IdentityUserToken<string>>()
            .Property(p => p.Value)
            .HasColumnName("value")
            .HasMaxLength(100);
    }
}