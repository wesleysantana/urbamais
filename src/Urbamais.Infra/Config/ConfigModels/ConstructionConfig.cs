using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Construction;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class ConstructionConfig : ConfigBase<Construction>
{
    public ConstructionConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Construction>()
            .Property(x => x.CompanieId)
            .HasColumnName("companie_id")
            .IsRequired();

        builder.Entity<Construction>()
            .HasOne(x => x.Companie)
            .WithMany(x => x.Constructions)
            .HasForeignKey(x => x.CompanieId);

        builder.Entity<Construction>()
            .OwnsOne(x => x.Description)
            .Property(x => x.Value)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(255);
    }
}