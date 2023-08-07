using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PhoneConfig : ConfigBase<Telefone>
{
    public PhoneConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Telefone>()
            .Property(x => x.Numero)
            .HasColumnName("number")
            .IsRequired()
            .HasMaxLength(20);
    }
}