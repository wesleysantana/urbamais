using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Suprimento;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CompraConfig
{
    public CompraConfig(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Compra>()
            .ToTable("compra");

        builder.Entity<Compra>()
            .Property(x => x.PedidoId)
            .HasColumnName("pedido_id")
            .IsRequired();

        builder.Entity<Compra>()
            .Property(x => x.InsumoId)
            .HasColumnName("insumo_id")
            .IsRequired();

        builder.Entity<Compra>()
            .HasKey(x => new { x.PedidoId, x.InsumoId });

        builder.Entity<Compra>()
            .Property(x => x.FornecedorId)
            .HasColumnName("fornecedor_id")
            .IsRequired();

        builder.Entity<Compra>()
            .Property(x => x.Quantidade)
            .HasColumnName("quantidade")
            .IsRequired();

        builder.Entity<Compra>()
            .Property(x => x.ValorUnitario)
            .HasColumnName("valor_unitario")
            .IsRequired();

        builder.Entity<Compra>()
            .Property(x => x.DataEntrega)
            .HasColumnName("data_entrega");

        builder.Entity<Compra>()
            .Property(x => x.LocaEntregaId)
            .HasColumnName("local_entrega_id");

        builder.Entity<Compra>()
            .HasOne(x => x.LocalEntrega)
            .WithMany(x => x.Compras)
            .HasForeignKey(x => x.LocaEntregaId);

        //#region Endereço

        //builder.Entity<Compra>()
        //    .OwnsOne(x => x.LocalEntrega)
        //    .Property(x => x.Bairro)
        //    .HasColumnName("bairro")
        //    .HasMaxLength(100)
        //    .IsRequired();

        //builder.Entity<Compra>()
        //    .OwnsOne(x => x.LocalEntrega)
        //    .Property(x => x.Logradouro)
        //    .HasColumnName("logradouro")
        //    .HasMaxLength(150)
        //    .IsRequired();

        //builder.Entity<Compra>()
        //    .OwnsOne(x => x.LocalEntrega)
        //    .Property(x => x.Complemento)
        //    .HasColumnName("complemento")
        //    .HasMaxLength(100);

        //builder.Entity<Compra>()
        //    .OwnsOne(x => x.LocalEntrega)
        //    .Property(x => x.Numero)
        //    .HasColumnName("numero")
        //    .HasMaxLength(10)
        //    .IsRequired();

        //builder.Entity<Compra>()
        //    .OwnsOne(x => x.LocalEntrega)
        //    .Property(x => x.Cep)
        //    .HasColumnName("cep")
        //    .IsRequired()
        //    .HasMaxLength(8);

        //builder.Entity<Compra>()
        //    .OwnsOne(x => x.LocalEntrega)
        //    .Property(x => x.CidadeId)
        //    .HasColumnName("cidade_id")
        //    .IsRequired();

        //#endregion Endereço

    }
}