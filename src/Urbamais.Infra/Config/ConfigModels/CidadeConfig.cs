using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CidadeConfig : ConfigBase<Cidade>
{
    public CidadeConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Cidade>()
            .OwnsOne(x => x.Nome)
            .Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Cidade>()
            .Property(x => x.Uf)
            .HasColumnName("uf");
    }
}