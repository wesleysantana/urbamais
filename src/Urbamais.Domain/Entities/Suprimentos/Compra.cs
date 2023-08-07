using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Domain.Entities.Suprimentos;

public class Compra
{
    public int PedidoId { get; private set; }
    public virtual Pedido? Pedido { get; private set; }
    public int InsumoId { get; private set; }
    public virtual Insumo? Insumo { get; private set; }
    public int FornecedorId { get; private set; }
    public virtual Fornecedor? Fornecedor { get; private set; }
    public double Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public DateTime? DataEntrega { get; private set; }
    public int? LocalEntregaId { get; private set; }
    public virtual Endereco? LocalEntrega { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Compra()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Compra(int pedidoId, int insumoId, int fornecedorId, double quantidade,
        decimal valorUnitario, DateTime dataEntrega, int localEntregaId)
    {
        PedidoId = pedidoId;
        InsumoId = insumoId;
        FornecedorId = fornecedorId;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        DataEntrega = dataEntrega;
        LocalEntregaId = localEntregaId;
    }
}