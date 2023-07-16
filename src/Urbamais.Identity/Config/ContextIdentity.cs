using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Urbamais.Identity.Config.ConfigModels;

namespace Urbamais.Identity.Config;

public class ContextIdentity : IdentityDbContext<ApplicationUser>
{
    public ContextIdentity(DbContextOptions<ContextIdentity> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        IdentityRoleClaimConfig.Config(modelBuilder);
        IdentityRoleConfig.Config(modelBuilder);
        IdentityUserClaimConfig.Config(modelBuilder);
        IdentityUserConfig.Config(modelBuilder);
        IdentityUserLoginConfig.Config(modelBuilder);
        IdentityUserRoleConfig.Config(modelBuilder);
        IdentityUserTokenConfig.Config(modelBuilder);
    }
}