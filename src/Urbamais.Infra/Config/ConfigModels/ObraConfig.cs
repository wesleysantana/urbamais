using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class ObraConfig : ConfigBase<Obra>
{
    public ObraConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Obra>()
            .OwnsOne(x => x.Descricao)
            .Property(x => x.Value)
            .HasColumnName("descricao")
            .IsRequired()
            .HasMaxLength(255);
    }
}