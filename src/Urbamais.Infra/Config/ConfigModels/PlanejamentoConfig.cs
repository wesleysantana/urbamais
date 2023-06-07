using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class PlanejamentoConfig : ConfigBase<Planejamento>
{
    public PlanejamentoConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Planejamento>()
            .Property(x => x.ObraId)
            .HasColumnName("obra_id")
            .IsRequired();

        builder.Entity<Planejamento>()
            .HasOne(x => x.Obra)
            .WithMany(x => x.Planejamentos)
            .HasForeignKey(x => x.ObraId);
    }
}