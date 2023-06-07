using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Urbamais.Domain.Entities.Planejamento;

public class PlanejamentoInsumo : BaseValidate, IEntity
{
    public int PlanejamentoId { get; private set; }
    public virtual Planejamento? Planejamento { get; private set; }
    public int InsumoId { get; private set; }
    public virtual Insumo? Insumo { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public double Quantidade { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }

    protected PlanejamentoInsumo()
    {
    }

    public PlanejamentoInsumo(int planejamentoId, int insumoId, decimal valorUnitario,
        double quantidade, DateTime dataInicio, DateTime dataFim)
    {
        PlanejamentoId = planejamentoId;
        InsumoId = insumoId;
        ValorUnitario = valorUnitario;
        Quantidade = quantidade;
        DataInicio = dataInicio;
        DataFim = dataFim;

        Validar();
    }

    private void Validar()
    {
        Validate(this, new PlanejamentoInsumoValidator());
    }

    public void Update(int? planejamentoId = null, int? insumoId = null, decimal? valorUnitario = null,
        double? quantidade = null, DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        if (planejamentoId is not null) PlanejamentoId = (int)planejamentoId;
        if (insumoId is not null) InsumoId = (int)insumoId;
        if (valorUnitario is not null) ValorUnitario = (decimal)valorUnitario;
        if (quantidade is not null) Quantidade = (double)quantidade;
        if (dataInicio is not null) DataInicio = (DateTime)dataInicio;
        if (dataFim is not null) DataFim = (DateTime)dataFim;

        Validar();
    }

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

            RuleFor(x => x.ValorUnitario)
                .NotNull()
                .Must(x => x > 0);

            RuleFor(x => x.Quantidade)
                .NotNull()
                .Must(x => x > 0);

            RuleFor(x => x.DataInicio)
                .Must(date => date != default);

            RuleFor(x => x.DataFim)
                .Must(date => date != default);

            RuleFor(x => x.DataFim)
                .Must((objeto, dataFinal) => dataFinal > objeto.DataInicio);
        }
    }
}