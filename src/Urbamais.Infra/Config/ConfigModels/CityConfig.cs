using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CityConfig : ConfigBase<Cidade>
{
    public CityConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Cidade>()
            .OwnsOne(x => x.Nome)
            .Property(x => x.Value)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Cidade>()
            .Property(x => x.Uf)
            .HasColumnName("uf");
    }
}