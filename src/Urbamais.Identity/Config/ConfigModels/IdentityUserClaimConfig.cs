using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityUserClaimConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityUserClaim<string>>().ToTable("usuarios_permissoes");

        builder.Entity<IdentityUserClaim<string>>()
            .Property(p => p.Id)
            .HasColumnName("id");

        builder.Entity<IdentityUserClaim<string>>()
            .Property(p => p.UserId)
            .HasColumnName("user_id");

        builder.Entity<IdentityUserClaim<string>>()
            .Property(p => p.ClaimType)
            .HasColumnName("claim_type");

        builder.Entity<IdentityUserClaim<string>>()
            .Property(p => p.ClaimValue)
            .HasColumnName("claim_value");
    }
}