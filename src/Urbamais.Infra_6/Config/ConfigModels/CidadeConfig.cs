using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CidadeConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<Cidade>().ToTable("cidade");
        builder.Entity<Cidade>().HasKey(x => x.Id);

        builder.Entity<Cidade>()
            .OwnsOne(x => x.Nome)
            .Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(255);              
    }
}