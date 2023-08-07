using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class AddressConfig : ConfigBase<Address>
{
    public AddressConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Address>()
            .Property(x => x.Logradouro)
            .HasColumnName("thoroughfare")
            .IsRequired()
            .HasMaxLength(150);

        builder.Entity<Address>()
            .Property(x => x.Numero)
            .HasColumnName("number")
            .IsRequired()
            .HasMaxLength(10);

        builder.Entity<Address>()
            .Property(x => x.Complemento)
            .HasColumnName("complement")
            .HasMaxLength(100);

        builder.Entity<Address>()
            .Property(x => x.Bairro)
            .HasColumnName("neighborhood")
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Address>()
            .Property(x => x.CodigoPostal)
            .HasColumnName("zip_code")
            .IsRequired()
            .HasMaxLength(8);

        builder.Entity<Address>()
            .Property(x => x.CityId)
            .HasColumnName("city_id")
            .IsRequired();

        builder.Entity<Address>()
            .HasOne(x => x.City)
            .WithMany(x => x.Address)
            .HasForeignKey(x => x.CityId);
    }
}