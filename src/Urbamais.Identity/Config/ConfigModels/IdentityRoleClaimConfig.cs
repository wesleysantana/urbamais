using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config.ConfigModels;

internal class IdentityRoleClaimConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<IdentityRoleClaim<string>>().ToTable("perfis_permissoes");

        builder.Entity<IdentityRoleClaim<string>>()
            .Property(p => p.Id)
            .HasColumnName("id");

        builder.Entity<IdentityRoleClaim<string>>()
            .Property(p => p.RoleId)
            .HasColumnName("perfil_id");

        builder.Entity<IdentityRoleClaim<string>>()
            .Property(p => p.ClaimType)
            .HasColumnName("tipo_permissao");

        builder.Entity<IdentityRoleClaim<string>>()
            .Property(p => p.ClaimValue)
            .HasColumnName("valor_permissao");
    }
}