using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Obra;
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
            .Property(x => x.EmpresaId)
            .HasColumnName("empresa_id")
            .IsRequired();

        builder.Entity<Obra>()
            .HasOne(x => x.Empresa)
            .WithMany(x => x.Obras)
            .HasForeignKey(x => x.EmpresaId);

        builder.Entity<Obra>()
            .OwnsOne(x => x.Descricao)
            .Property(x => x.Descricao)
            .HasColumnName("descricao")
            .IsRequired()
            .HasMaxLength(255);
    }
}