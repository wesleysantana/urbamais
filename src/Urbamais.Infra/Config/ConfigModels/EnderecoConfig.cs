using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class EnderecoConfig : ConfigBase<Endereco>
{
    public EnderecoConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Endereco>()
            .Property(x => x.Logradouro)
            .HasColumnName("logradouro")
            .IsRequired()
            .HasMaxLength(150);

        builder.Entity<Endereco>()
            .Property(x => x.Numero)
            .HasColumnName("numero")
            .IsRequired()
            .HasMaxLength(10);

        builder.Entity<Endereco>()
            .Property(x => x.Complemento)
            .HasColumnName("complemento")
            .HasMaxLength(100);

        builder.Entity<Endereco>()
            .Property(x => x.Bairro)
            .HasColumnName("bairro")
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Endereco>()
            .Property(x => x.Cep)
            .HasColumnName("cep")
            .IsRequired()
            .HasMaxLength(8);

        builder.Entity<Endereco>()
            .Property(x => x.CidadeId)
            .HasColumnName("cidade_id")
            .IsRequired();

        builder.Entity<Endereco>()
            .HasOne(x => x.Cidade)
            .WithMany(x => x.Enderecos)
            .HasForeignKey(x => x.CidadeId);
    }
}