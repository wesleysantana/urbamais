using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.Fornecedores;

namespace Urbamais.Domain.Entities.Suprimentos;

public class Concorrencia : BaseValidate, IEntity
{
    public int Id { get; private set; }
    public int PedidoId { get; private set; }
    public virtual Pedido? Pedido { get; private set; }
    public int FornecedorId { get; private set; }
    public virtual Fornecedor? Fornecedor { get; private set; }
    public ValorMonetario ValorUnitario { get; private set; }
    public Quantidade Quantidade { get; private set; }
    public string FormaPagamento { get; private set; }
    public string CondicaoPagamento { get; private set; }
    public ValorMonetario ValorEntrega { get; private set; }
    public DateTime PrazoEntrega { get; private set; }
    public string Observacao { get; private set; }

    public Concorrencia(int id, int pedidoId, int fornecedorId, ValorMonetario valorUnitario, Quantidade quantidade,
        string formaPagamento, string condicaoPagamento, ValorMonetario valorEntrega, DateTime prazoEntrega, string observacao)
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

        Validate();

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(ValorUnitario.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(ValorEntrega.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Quantidade.ValidationResult!.Errors);

        Validate(this, new ConcorrenciaValidator());
    }

    public void Update(int? pedidoId = null, int? fornecedorId = null, ValorMonetario? valorUnitario = null,
        Quantidade? quantidade = null, string? formaPagamento = null, string? condicaoPagamento = null,
        ValorMonetario? valorEntrega = null, DateTime? prazoEntrega = null, string? observacao = null)
    {
        var memento = CreateMemento();

        if (pedidoId is not null) PedidoId = (int)pedidoId;
        if (fornecedorId is not null) FornecedorId = (int)fornecedorId;
        if (valorUnitario is not null) ValorUnitario = valorUnitario;
        if (quantidade is not null) Quantidade = quantidade;
        if (string.IsNullOrWhiteSpace(formaPagamento)) FormaPagamento = formaPagamento!;
        if (string.IsNullOrWhiteSpace(condicaoPagamento)) CondicaoPagamento = condicaoPagamento!;
        if (valorEntrega is not null) ValorEntrega = valorEntrega;
        if (prazoEntrega is not null) PrazoEntrega = (DateTime)prazoEntrega;
        if (string.IsNullOrWhiteSpace(observacao)) Observacao = observacao!;

        Validate();

        if (!IsValid)
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

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is Concorrencia concorrencia &&
               Id == concorrencia.Id &&
               PedidoId == concorrencia.PedidoId &&
               FornecedorId == concorrencia.FornecedorId &&
               EqualityComparer<ValorMonetario>.Default.Equals(ValorUnitario, concorrencia.ValorUnitario) &&
               EqualityComparer<Quantidade>.Default.Equals(Quantidade, concorrencia.Quantidade) &&
               FormaPagamento == concorrencia.FormaPagamento &&
               CondicaoPagamento == concorrencia.CondicaoPagamento &&
               EqualityComparer<ValorMonetario>.Default.Equals(ValorEntrega, concorrencia.ValorEntrega) &&
               PrazoEntrega == concorrencia.PrazoEntrega &&
               Observacao == concorrencia.Observacao;
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(Id);
        hash.Add(PedidoId);
        hash.Add(FornecedorId);
        hash.Add(ValorUnitario);
        hash.Add(Quantidade);
        hash.Add(FormaPagamento);
        hash.Add(CondicaoPagamento);
        hash.Add(ValorEntrega);
        hash.Add(PrazoEntrega);
        hash.Add(Observacao);
        return hash.ToHashCode();
    }

    public static bool operator ==(Concorrencia left, Concorrencia right) => left.Equals(right);

    public static bool operator !=(Concorrencia left, Concorrencia right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class ConcorrenciaValidator : AbstractValidator<Concorrencia>
    {
        public ConcorrenciaValidator()
        {
            RuleFor(x => x.FormaPagamento)
                .NotEmpty();

            RuleFor(x => x.CondicaoPagamento)
                .NotEmpty();

            RuleFor(x => x.Observacao)
                .MaximumLength(255);
        }
    }
}