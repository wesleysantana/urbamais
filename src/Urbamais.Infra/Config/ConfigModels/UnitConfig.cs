using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planning;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class UnitConfig : ConfigBase<Unit>
{
    public UnitConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Unit>()
            .Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<Unit>()
            .Property(x => x.Acronym)
            .HasColumnName("acronym")
            .HasMaxLength(10)
            .IsRequired();
    }
}