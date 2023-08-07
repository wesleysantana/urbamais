using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class EquipmentConfig : ConfigBase<Equipamento>
{
    public EquipmentConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Equipamento>()
            .OwnsOne(x => x.Nome)
            .Property(x => x.Value)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Entity<Equipamento>()
            .OwnsOne(x => x.Descricao)
            .Property(x => x.Value)
            .HasColumnName("description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Entity<Equipamento>()
            .HasMany(x => x.Fornecedores)
            .WithMany(x => x.Equipamentos)
            .UsingEntity<Dictionary<string, object>>(
                "suppliers_equipaments",
                x => x.HasOne<Fornecedor>().WithMany().HasForeignKey("supplier_id"),
                x => x.HasOne<Equipamento>().WithMany().HasForeignKey("equipament_id")
            );
    }
}