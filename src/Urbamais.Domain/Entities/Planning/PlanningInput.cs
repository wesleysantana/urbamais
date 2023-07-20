using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Urbamais.Domain.Entities.Planning;

public class PlanningInput : BaseValidate, IEntity
{
    public int NumericalOrder { get; private set; }
    public int PlanningId { get; private set; }
    public virtual Planning? Planning { get; private set; }
    public int InputId { get; private set; }
    public virtual Input? Input { get; private set; }
    public decimal UnitaryValue { get; private set; }
    public int UnitId { get; private set; }
    public virtual Unit? Unit { get; private set; }
    public double Amount { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime FinalDate { get; private set; }

    protected PlanningInput()
    {
    }

    public PlanningInput(int planningId, int inputId, int unitId, decimal unitaryValue,
        double amount, DateTime startDate, DateTime finalDate)
    {
        PlanningId = planningId;
        InputId = inputId;
        UnitId = unitId;
        UnitaryValue = unitaryValue;
        Amount = amount;
        StartDate = startDate;
        FinalDate = finalDate;

        Validate();
    }

    private void Validate()
    {
        Validate(this, new PlanningInputValidator());
    }

    public void Update(int? planningId = null, int? inputId = null, int? unitId = null, decimal? unitaryValue = null,
        double? amount = null, DateTime? startDate = null, DateTime? finalDate = null)
    {
        if (planningId is not null) PlanningId = (int)planningId;
        if (inputId is not null) InputId = (int)inputId;
        if (unitId is not null) UnitId = (int)unitId;
        if (unitaryValue is not null) UnitaryValue = (decimal)unitaryValue;
        if (amount is not null) Amount = (double)amount;
        if (startDate is not null) StartDate = (DateTime)startDate;
        if (finalDate is not null) FinalDate = (DateTime)finalDate;

        Validate();
    }

    private class PlanningInputValidator : AbstractValidator<PlanningInput>
    {
        public PlanningInputValidator()
        {
            RuleFor(x => x.PlanningId)
                .NotNull()
                .NotEqual(0);

            RuleFor(x => x.InputId)
                .NotNull()
                .NotEqual(0);

            RuleFor(x => x.UnitId)
                .NotNull()
                .NotEqual(0);

            RuleFor(x => x.UnitaryValue)
                .NotNull()
                .Must(x => x > 0);

            RuleFor(x => x.Amount)
                .NotNull()
                .Must(x => x > 0);

            RuleFor(x => x.StartDate)
                .Must(date => date != default);

            RuleFor(x => x.FinalDate)
                .Must(date => date != default);

            RuleFor(x => x.FinalDate)
                .Must((objeto, dataFinal) => dataFinal > objeto.StartDate);
        }
    }
}