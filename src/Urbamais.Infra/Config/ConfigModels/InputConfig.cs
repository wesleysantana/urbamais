using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Planning;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class InputConfig : ConfigBase<Input>
{
    public InputConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Input>()
            .Property(x => x.Type)
            .HasColumnName("typo")
            .IsRequired();

        builder.Entity<Input>()
            .OwnsOne(x => x.Name)
            .Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Input>()
            .Property(x => x.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Input>()
            .Property(x => x.UnitId)
            .HasColumnName("unit_id")
            .IsRequired();

        builder.Entity<Input>()
            .HasOne(x => x.Unit)
            .WithMany(x => x.Inputs)
            .HasForeignKey(x => x.UnitId);
    }
}