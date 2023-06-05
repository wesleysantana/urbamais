using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class TelefoneConfig : ConfigBase<Telefone>
{
    public TelefoneConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Telefone>()
            .Property(x => x.Numero)
            .HasColumnName("numero")
            .IsRequired()
            .HasMaxLength(20);
    }
}