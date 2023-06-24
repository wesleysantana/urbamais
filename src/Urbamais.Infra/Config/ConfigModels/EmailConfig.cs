using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class EmailConfig : ConfigBase<Email>
{
    public EmailConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Email>()
            .Property(x => x.Endereco)
            .HasColumnName("endereco")
            .IsRequired()
            .HasMaxLength(255);
    }
}