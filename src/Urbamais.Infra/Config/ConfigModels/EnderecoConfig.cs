using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Infra.Config.ConfigModels;

internal class EnderecoConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<Endereco>().ToTable("endereco");
        builder.Entity<Endereco>().HasKey(x => x.Id);

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
            .HasOne<Cidade>()
            .WithMany(x => x.Enderecos)
            .IsRequired();
    }
}