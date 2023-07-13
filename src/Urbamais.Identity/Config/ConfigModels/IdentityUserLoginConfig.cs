using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityUserLoginConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityUserLogin<string>>().ToTable("identity_user_login");

        builder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });

        builder.Entity<IdentityUserLogin<string>>()
            .Property(p => p.LoginProvider)
            .HasColumnName("login_provider");

        builder.Entity<IdentityUserLogin<string>>()
           .Property(p => p.ProviderKey)
           .HasColumnName("provider_key");

        builder.Entity<IdentityUserLogin<string>>()
           .Property(p => p.ProviderDisplayName)
           .HasColumnName("provider_display_name");

        builder.Entity<IdentityUserLogin<string>>()
           .Property(p => p.UserId)
           .HasColumnName("user_id");
    }
}