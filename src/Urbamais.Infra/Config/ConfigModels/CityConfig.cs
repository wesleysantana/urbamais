using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CityConfig : ConfigBase<City>
{
    public CityConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<City>()
            .OwnsOne(x => x.Name)
            .Property(x => x.Value)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<City>()
            .Property(x => x.Uf)
            .HasColumnName("uf");
    }
}