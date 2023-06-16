using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityUserRoleConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<string>>().ToTable("usuarios_perfis");

        builder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });

        builder.Entity<IdentityUserRole<string>>()
            .Property(p => p.UserId)
            .HasColumnName("usuario_id");

        builder.Entity<IdentityUserRole<string>>()
            .Property(p => p.RoleId)
            .HasColumnName("perfil_id");
    }
}