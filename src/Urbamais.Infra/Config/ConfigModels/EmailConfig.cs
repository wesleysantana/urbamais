using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Infra.Config.ConfigModels;

internal class EmailConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<Email>().ToTable("email");
        builder.Entity<Email>().HasKey(x => x.Id);

        builder.Entity<Email>()
            .Property(x => x.Endereco)
            .HasColumnName("endereco")
            .IsRequired()
            .HasMaxLength(255);
    }
}