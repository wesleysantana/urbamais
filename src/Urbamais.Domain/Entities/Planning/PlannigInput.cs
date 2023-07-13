using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Urbamais.Domain.Entities.Planning;

public class PlannigInput : BaseValidate, IEntity
{
    public int PlannigId { get; private set; }
    public virtual Planning? Planning { get; private set; }
    public int InputId { get; private set; }
    public virtual Input? Input { get; private set; }
    public decimal UnitaryValue { get; private set; }
    public double Amount { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime FinalDate { get; private set; }

    protected PlannigInput()
    {
    }

    public PlannigInput(int plannigId, int inputId, decimal unitaryValue,
        double amount, DateTime startDate, DateTime finalDate)
    {
        PlannigId = plannigId;
        InputId = inputId;
        UnitaryValue = unitaryValue;
        Amount = amount;
        StartDate = startDate;
        FinalDate = finalDate;

        Validate();
    }

    private void Validate()
    {
        Validate(this, new PlannigInputValidator());
    }

    public void Update(int? plannigId = null, int? inputId = null, decimal? unitaryValue = null,
        double? amount = null, DateTime? startDate = null, DateTime? finalDate = null)
    {
        if (plannigId is not null) PlannigId = (int)plannigId;
        if (inputId is not null) InputId = (int)inputId;
        if (unitaryValue is not null) UnitaryValue = (decimal)unitaryValue;
        if (amount is not null) Amount = (double)amount;
        if (startDate is not null) StartDate = (DateTime)startDate;
        if (finalDate is not null) FinalDate = (DateTime)finalDate;

        Validate();
    }

    private class PlannigInputValidator : AbstractValidator<PlannigInput>
    {
        public PlannigInputValidator()
        {
            RuleFor(x => x.PlannigId)
                .NotNull()
                .NotEqual(0);

            RuleFor(x => x.InputId)
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