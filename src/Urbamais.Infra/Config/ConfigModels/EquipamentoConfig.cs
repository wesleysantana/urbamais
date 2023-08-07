using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class EquipamentoConfig : ConfigBase<Equipamento>
{
    public EquipamentoConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Equipamento>()
            .OwnsOne(x => x.Nome)
            .Property(x => x.Value)
            .HasColumnName("nome")
            .HasMaxLength(255)
            .IsRequired();

        builder.Entity<Equipamento>()
            .OwnsOne(x => x.Descricao)
            .Property(x => x.Value)
            .HasColumnName("descricao")
            .HasMaxLength(255)
            .IsRequired();

        builder.Entity<Equipamento>()
            .HasMany(x => x.Fornecedores)
            .WithMany(x => x.Equipamentos)
            .UsingEntity<Dictionary<string, object>>(
                "fornecedores_equipamentos",
                x => x.HasOne<Fornecedor>().WithMany().HasForeignKey("fornecedor_id"),
                x => x.HasOne<Equipamento>().WithMany().HasForeignKey("equipamento_id")
            );
    }
}