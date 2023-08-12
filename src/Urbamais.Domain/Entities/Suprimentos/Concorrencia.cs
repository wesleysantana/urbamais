using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using Urbamais.Domain.Entities.Fornecedores;

namespace Urbamais.Domain.Entities.Suprimentos;

public class Concorrencia : BaseValidate, IEntity
{
    public int Id { get; private set; }
    public int PedidoId { get; private set; }
    public virtual Pedido? Pedido { get; private set; }
    public int FornecedorId { get; private set; }
    public virtual Fornecedor? Fornecedor { get; private set; }
    public ValorUnitario ValorUnitario { get; private set; }
    public double Quantidade { get; private set; }
    public string FormaPagamento { get; private set; }
    public string CondicaoPagamento { get; private set; }
    public ValorUnitario ValorEntrega { get; private set; }
    public DateTime PrazoEntrega { get; private set; }
    public string Observacao { get; private set; }

    public Concorrencia(int id, int pedidoId, int fornecedorId, ValorUnitario valorUnitario, double quantidade,
        string formaPagamento, string condicaoPagamento, ValorUnitario valorEntrega, DateTime prazoEntrega, string observacao)
    {
        Id = id;
        PedidoId = pedidoId;
        FornecedorId = fornecedorId;
        ValorUnitario = valorUnitario;
        Quantidade = quantidade;
        FormaPagamento = formaPagamento;
        CondicaoPagamento = condicaoPagamento;
        ValorEntrega = valorEntrega;
        PrazoEntrega = prazoEntrega;
        Observacao = observacao;
    }

    public void Update(int userModificationId, int? pedidoId = null, int? fornecedorId = null, ValorUnitario? valorUnitario = null,
        double? quantidade = null, string? formaPagamento = null, string? condicaoPagamento = null, ValorUnitario? valorEntrega = null,
        DateTime? prazoEntrega = null, string? observacao = null)
    {
        var memento = CreateMemento();

        if (pedidoId is not null) PedidoId = (int)pedidoId;
        if (fornecedorId is not null) FornecedorId = (int)fornecedorId;
        if (valorUnitario is not null) ValorUnitario = valorUnitario;
        if (quantidade is not null) Quantidade = (double)quantidade;
        if (string.IsNullOrWhiteSpace(formaPagamento)) FormaPagamento = formaPagamento!;
        if (string.IsNullOrWhiteSpace(condicaoPagamento)) CondicaoPagamento = condicaoPagamento!;
        if (valorEntrega is not null) ValorEntrega = valorEntrega;
        if (prazoEntrega is not null) PrazoEntrega = (DateTime)prazoEntrega;
        if (string.IsNullOrWhiteSpace(observacao)) Observacao = observacao!;

        RestoreMemento(memento);
    }

    #region Memento

    private object CreateMemento()
    {
        return new
        {
            PedidoId,
            FornecedorId,
            ValorUnitario,
            Quantidade,
            FormaPagamento,
            CondicaoPagamento,
            ValorEntrega,
            PrazoEntrega,
            Observacao
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        PedidoId = state.IdPedido;
        FornecedorId = state.FornecedorId;
        ValorUnitario = state.ValorUnitario;
        Quantidade = state.Quantidade;
        FormaPagamento = state.FormaPagamento;
        CondicaoPagamento = state.CondicaoPagamento;
        ValorEntrega = state.ValorEntrega;
        PrazoEntrega = state.PrazoEntrega;
        Observacao = state.Observacao;
    }

    #endregion Memento
}