using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityUserClaimConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityUserClaim<string>>().ToTable("identity_user_claim");

        builder.Entity<IdentityUserClaim<string>>()
            .Property(p => p.Id)
            .HasColumnName("id")
            .HasMaxLength(100);

        builder.Entity<IdentityUserClaim<string>>()
            .Property(p => p.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(100);

        builder.Entity<IdentityUserClaim<string>>()
            .Property(p => p.ClaimType)
            .HasColumnName("claim_type")
            .HasMaxLength(100);

        builder.Entity<IdentityUserClaim<string>>()
            .Property(p => p.ClaimValue)
            .HasColumnName("claim_value")
            .HasMaxLength(100);
    }
}