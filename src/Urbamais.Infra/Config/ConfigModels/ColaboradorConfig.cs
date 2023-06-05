using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Fornecedor;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class ColaboradorConfig : ConfigBase<Colaborador>
{
    public ColaboradorConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Colaborador>()
           .Property(x => x.Nome)
           .HasColumnName("nome")
           .IsRequired()
           .HasMaxLength(255);

        builder.Entity<Colaborador>()
            .Property(x => x.Cpf)
            .HasColumnName("cpf")
            .IsRequired()
            .HasMaxLength(11);
    }
}