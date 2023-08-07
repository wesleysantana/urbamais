using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class UnitConfig : ConfigBase<Unidade>
{
    public UnitConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Unidade>()
            .Property(x => x.Descricao)
            .HasColumnName("description")
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<Unidade>()
            .Property(x => x.Sigla)
            .HasColumnName("acronym")
            .HasMaxLength(10)
            .IsRequired();
    }
}