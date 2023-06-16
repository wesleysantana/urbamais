using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityUserTokenConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityUserToken<string>>().ToTable("token");

        builder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

        builder.Entity<IdentityUserToken<string>>()
            .Property(p => p.UserId)
            .HasColumnName("usuario_id");

        builder.Entity<IdentityUserToken<string>>()
            .Property(p => p.UserId)
            .HasColumnName("usuario_id");

        builder.Entity<IdentityUserToken<string>>()
            .Property(p => p.LoginProvider)
            .HasColumnName("login_provider");

        builder.Entity<IdentityUserToken<string>>()
            .Property(p => p.Name)
            .HasColumnName("nome");

        builder.Entity<IdentityUserToken<string>>()
            .Property(p => p.Value)
            .HasColumnName("valor");
    }
}