using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class AddressConfig : ConfigBase<Endereco>
{
    public AddressConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Endereco>()
            .Property(x => x.Logradouro)
            .HasColumnName("thoroughfare")
            .IsRequired()
            .HasMaxLength(150);

        builder.Entity<Endereco>()
            .Property(x => x.Numero)
            .HasColumnName("number")
            .IsRequired()
            .HasMaxLength(10);

        builder.Entity<Endereco>()
            .Property(x => x.Complemento)
            .HasColumnName("complement")
            .HasMaxLength(100);

        builder.Entity<Endereco>()
            .Property(x => x.Bairro)
            .HasColumnName("neighborhood")
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Endereco>()
            .Property(x => x.CodigoPostal)
            .HasColumnName("zip_code")
            .IsRequired()
            .HasMaxLength(8);

        builder.Entity<Endereco>()
            .Property(x => x.CidadeId)
            .HasColumnName("city_id")
            .IsRequired();

        builder.Entity<Endereco>()
            .HasOne(x => x.Cidade)
            .WithMany(x => x.Endereco)
            .HasForeignKey(x => x.CidadeId);
    }
}