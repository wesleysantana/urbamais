using Core.ValueObjects;
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
    public Quantidade Quantidade { get; private set; }
    public ValorMonetario ValorUnitario { get; private set; }
    public DateTime? DataEntrega { get; private set; }
    public int? LocalEntregaId { get; private set; }
    public virtual Endereco? LocalEntrega { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Compra()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Compra(int pedidoId, int insumoId, int fornecedorId, Quantidade quantidade,
        ValorMonetario valorUnitario, DateTime dataEntrega, int localEntregaId)
    {
        PedidoId = pedidoId;
        InsumoId = insumoId;
        FornecedorId = fornecedorId;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        DataEntrega = dataEntrega;
        LocalEntregaId = localEntregaId;
    }

    public void Update(int? pedidoId = null, int? insumoId = null, int? fornecedorId = null, Quantidade? quantidade = null,
        ValorMonetario? valorUnitario = null, DateTime? dataEntrega = null, int? localEntregaId = null)
    { 
        if (pedidoId is not null) PedidoId = (int)pedidoId;
        if (insumoId is not null) InsumoId = (int)insumoId;
        if (fornecedorId is not null) FornecedorId = (int)fornecedorId;
        if (quantidade is not null) Quantidade = quantidade;
        if (valorUnitario is not null) ValorUnitario = valorUnitario;
        if (dataEntrega is not null) DataEntrega = dataEntrega;
        if (localEntregaId is not null) LocalEntregaId = localEntregaId;
    }
}