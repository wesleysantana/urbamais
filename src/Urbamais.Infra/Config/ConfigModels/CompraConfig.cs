using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Suprimentos;

namespace Urbamais.Infra.Config.ConfigModels;

internal class CompraConfig
{
    public CompraConfig(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Compra>().ToTable("compra");

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
            .Property(x => x.LocalEntregaId)
            .HasColumnName("local_entrega_id");

        builder.Entity<Compra>()
            .HasOne(x => x.LocalEntrega)
            .WithMany(x => x.Compras)
            .HasForeignKey(x => x.LocalEntregaId);
    }
}