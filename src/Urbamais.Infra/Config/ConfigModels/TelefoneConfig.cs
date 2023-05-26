using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Infra.Config.ConfigModels;

internal class TelefoneConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<Telefone>().ToTable("telefone");
        builder.Entity<Telefone>().HasKey(x => x.Id);

        builder.Entity<Telefone>()
            .Property(x => x.Numero)
            .HasColumnName("numero")
            .IsRequired()
            .HasMaxLength(20);
    }
}