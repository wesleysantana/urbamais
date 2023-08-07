using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class ConstructionConfig : ConfigBase<Obra>
{
    public ConstructionConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Obra>()
            .Property(x => x.EmpresaId)
            .HasColumnName("companie_id")
            .IsRequired();

        builder.Entity<Obra>()
            .HasOne(x => x.Empresa)
            .WithMany(x => x.Constructions)
            .HasForeignKey(x => x.EmpresaId);

        builder.Entity<Obra>()
            .OwnsOne(x => x.Descricao)
            .Property(x => x.Value)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(255);
    }
}