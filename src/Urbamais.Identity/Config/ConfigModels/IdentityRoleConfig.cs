using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityRoleConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().ToTable("identity_role");
        builder.Entity<IdentityRole>().HasKey(x => x.Id);

        builder.Entity<IdentityRole>()
            .Property(x => x.Id)
            .HasColumnName("id");

        builder.Entity<IdentityRole>()
            .Property(p => p.Name)
            .HasColumnName("name");

        builder.Entity<IdentityRole>()
            .Property(p => p.NormalizedName)
            .HasColumnName("normalized_name");

        builder.Entity<IdentityRole>()
           .Property(p => p.ConcurrencyStamp)
           .HasColumnName("concurrency_stamp");
    }
}