using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;

namespace Urbamais.Domain.Entities.Planejamentos;

public class PlanejamentoInsumo : BaseValidate, IEntity
{
    public int Id { get; private set; }
    public int PlanejamentoId { get; private set; }
    public virtual Planejamento? Planejamento { get; private set; }
    public int InsumoId { get; private set; }
    public virtual Insumo? Insumo { get; private set; }
    public ValorMonetario ValorUnitario { get; private set; }
    public int UnidadeId { get; private set; }
    public virtual Unidade? Unidade { get; private set; }
    public Quantidade Quantidade { get; private set; }
    public DateTime DataInicial { get; private set; }
    public DateTime DataFinal { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected PlanejamentoInsumo()
    { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
   

    public PlanejamentoInsumo(int planejamentoId, int insumoId, int unidadeId, ValorMonetario valorUnitario,
        Quantidade quantidade, DateTime dataInicial, DateTime dataFinal)
    {
        PlanejamentoId = planejamentoId;
        InsumoId = insumoId;
        UnidadeId = unidadeId;
        ValorUnitario = valorUnitario;
        Quantidade = quantidade;
        DataInicial = dataInicial;
        DataFinal = dataFinal;

        Validate();

        if (!IsValid)
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in properties)
                item.SetValue(this, default);
        }
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(ValorUnitario.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Quantidade.ValidationResult!.Errors);

        Validate(this, new PlanejamentoInsumoValidator());
    }

    public void Update(int? planejamentoId = null, int? insumoId = null, int? unidadeId = null, ValorMonetario? valorUnitario = null,
        Quantidade? quantidade = null, DateTime? dataInicial = null, DateTime? dataFinal = null)
    {
        var memento = CreateMemento();

        if (planejamentoId is not null) PlanejamentoId = (int)planejamentoId;
        if (insumoId is not null) InsumoId = (int)insumoId;
        if (unidadeId is not null) UnidadeId = (int)unidadeId;
        if (valorUnitario is not null) ValorUnitario = valorUnitario;
        if (quantidade is not null) Quantidade = quantidade;
        if (dataInicial is not null) DataInicial = (DateTime)dataInicial;
        if (dataFinal is not null) DataFinal = (DateTime)dataFinal;

        Validate();

        if (!IsValid)
            RestoreMemento(memento);
    }

    #region Memento

    private object CreateMemento()
    {
        return new
        {
            PlanejamentoId,
            InsumoId,
            UnidadeId,
            ValorUnitario,
            Quantidade,
            DataInicial,
            DataFinal
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        PlanejamentoId = state.PlanejamentoId;
        InsumoId = state.InsumoId;
        UnidadeId = state.UnidadeId;
        ValorUnitario = state.ValorUnitario;
        Quantidade = state.Quantidade;
        DataInicial = state.DataInicial;
        DataFinal = state.DataFinal;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() => $"Planejamento Insumo - PlanejamentoId: {PlanejamentoId}, Insumo: {InsumoId}-{Insumo?.Descricao}, " +
        $"";    

    public override bool Equals(object? obj)
    {
        return obj is PlanejamentoInsumo insumo &&
               Id == insumo.Id &&
               PlanejamentoId == insumo.PlanejamentoId &&
               InsumoId == insumo.InsumoId &&
               ValorUnitario == insumo.ValorUnitario &&
               UnidadeId == insumo.UnidadeId &&
               Quantidade == insumo.Quantidade &&
               DataInicial == insumo.DataInicial &&
               DataFinal == insumo.DataFinal;
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(Id);
        hash.Add(PlanejamentoId);
        hash.Add(InsumoId);
        hash.Add(ValorUnitario);
        hash.Add(UnidadeId);
        hash.Add(Quantidade);
        hash.Add(DataInicial);
        hash.Add(DataFinal);
        return hash.ToHashCode();
    }

    #endregion Sobrescrita Object

    private class PlanejamentoInsumoValidator : AbstractValidator<PlanejamentoInsumo>
    {
        public PlanejamentoInsumoValidator()
        {
            RuleFor(x => x.PlanejamentoId)
                .NotNull()
                .NotEqual(0);

            RuleFor(x => x.InsumoId)
                .NotNull()
                .NotEqual(0);

            RuleFor(x => x.UnidadeId)
                .NotNull()
                .NotEqual(0);           

            RuleFor(x => x.DataInicial)
                .Must(date => date != default);

            RuleFor(x => x.DataFinal)
                .Must(date => date != default);

            RuleFor(x => x.DataFinal)
                .Must((obj, dataFinal) => dataFinal > obj.DataInicial);
        }
    }
}