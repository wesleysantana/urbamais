using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using Urbamais.Domain.Entities.Fornecedores;

namespace Urbamais.Domain.Entities.Suprimentos;

public class Concorrencia : BaseValidate, IEntity
{
    public int Id { get; private set; }
    public int IdPedido { get; private set; }
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

    public Concorrencia(int id, int idPedido, int fornecedorId, ValorUnitario valorUnitario, double quantidade, 
        string formaPagamento, string condicaoPagamento, ValorUnitario valorEntrega, DateTime prazoEntrega, string observacao)
    {
        Id = id;
        IdPedido = idPedido;
        FornecedorId = fornecedorId;
        ValorUnitario = valorUnitario;
        Quantidade = quantidade;
        FormaPagamento = formaPagamento;
        CondicaoPagamento = condicaoPagamento;
        ValorEntrega = valorEntrega;
        PrazoEntrega = prazoEntrega;
        Observacao = observacao;
    }
}