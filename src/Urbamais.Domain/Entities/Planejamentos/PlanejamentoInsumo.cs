using Core.Domain.Interfaces;
using Core.SeedWork;
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
    public decimal ValorUnitario { get; private set; }
    public int UnidadeId { get; private set; }
    public virtual Unidade? Unidade { get; private set; }
    public double Quantidade { get; private set; }
    public DateTime DataInicial { get; private set; }
    public DateTime DataFinal { get; private set; }

    protected PlanejamentoInsumo()
    {
    }

    public PlanejamentoInsumo(int planejamentoId, int insumoId, int unidadeId, decimal valorUnitario,
        double quantidade, DateTime dataInicial, DateTime dataFinal)
    {
        PlanejamentoId = planejamentoId;
        InsumoId = insumoId;
        UnidadeId = unidadeId;
        ValorUnitario = valorUnitario;
        Quantidade = quantidade;
        DataInicial = dataInicial;
        DataFinal = dataFinal;

        Validate(this, new PlanejamentoInsumoValidator());

        if (!IsValid)
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in properties)
                item.SetValue(this, default);
        }
    }

    public void Update(int? planejamentoId = null, int? insumoId = null, int? unidadeId = null, decimal? valorUnitario = null,
        double? quantidade = null, DateTime? dataInicial = null, DateTime? dataFinal = null)
    {
        var memento = CreateMemento();

        if (planejamentoId is not null) PlanejamentoId = (int)planejamentoId;
        if (insumoId is not null) InsumoId = (int)insumoId;
        if (unidadeId is not null) UnidadeId = (int)unidadeId;
        if (valorUnitario is not null) ValorUnitario = (decimal)valorUnitario;
        if (quantidade is not null) Quantidade = (double)quantidade;
        if (dataInicial is not null) DataInicial = (DateTime)dataInicial;
        if (dataFinal is not null) DataFinal = (DateTime)dataFinal;

        Validate(this, new PlanejamentoInsumoValidator());

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

            RuleFor(x => x.ValorUnitario)
                .NotNull()
                .Must(x => x > 0);

            RuleFor(x => x.Quantidade)
                .NotNull()
                .Must(x => x > 0);

            RuleFor(x => x.DataInicial)
                .Must(date => date != default);

            RuleFor(x => x.DataFinal)
                .Must(date => date != default);

            RuleFor(x => x.DataFinal)
                .Must((objeto, dataFinal) => dataFinal > objeto.DataInicial);
        }
    }
}