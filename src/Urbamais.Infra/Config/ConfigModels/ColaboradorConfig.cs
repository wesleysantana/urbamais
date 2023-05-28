using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Fornecedor;

namespace Urbamais.Infra.Config.ConfigModels;

internal class ColaboradorConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<Colaborador>().ToTable("colaborador");
        builder.Entity<Colaborador>().HasKey(x => x.Id);

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