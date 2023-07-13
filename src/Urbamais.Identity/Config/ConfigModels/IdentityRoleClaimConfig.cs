using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityRoleClaimConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityRoleClaim<string>>().ToTable("identity_role_claim");

        builder.Entity<IdentityRoleClaim<string>>()
            .Property(p => p.Id)
            .HasColumnName("id");

        builder.Entity<IdentityRoleClaim<string>>()
            .Property(p => p.RoleId)
            .HasColumnName("role_id");

        builder.Entity<IdentityRoleClaim<string>>()
            .Property(p => p.ClaimType)
            .HasColumnName("claim_type");

        builder.Entity<IdentityRoleClaim<string>>()
            .Property(p => p.ClaimValue)
            .HasColumnName("claim_value");
    }
}