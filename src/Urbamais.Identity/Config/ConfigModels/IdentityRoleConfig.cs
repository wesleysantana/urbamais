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
            .HasColumnName("id")
            .HasMaxLength(100);

        builder.Entity<IdentityRole>()
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(100);

        builder.Entity<IdentityRole>()
            .Property(p => p.NormalizedName)
            .HasColumnName("normalized_name")
            .HasMaxLength(100);

        builder.Entity<IdentityRole>()
           .Property(p => p.ConcurrencyStamp)
           .HasColumnName("concurrency_stamp")
           .HasMaxLength(100);

        //builder.Entity<IdentityRole>().HasData(
        //       new IdentityRole
        //       {
        //           Name = "developer",
        //           Id = "af92719d-1d7f-4c80-aadc-ead1e2ab3a9d"
        //       }
        //   );
    }
}