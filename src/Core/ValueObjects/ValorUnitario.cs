using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public class ValorUnitarioCore : BaseValidate, IEntity
{
    public decimal Valor { get; private set; }

    public ValorUnitarioCore(decimal valor)
    {
        Valor = valor;

        Validate(this, new ValorUnitarioCoreValidator());

        if (!IsValid)
            Valor = default;
    }

    public void Update(decimal valor)
    {
        var memento = CreateMemento();

        Valor = valor;

        Validate(this, new ValorUnitarioCoreValidator());

        if (!IsValid)
            RestoreMemento(memento);
    }

    #region Memento

    private object CreateMemento()
    {
        return new
        {
            Valor
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Valor = state.Valor;
    }

    private class ValorUnitarioCoreValidator : AbstractValidator<ValorUnitarioCore>
    {
        public ValorUnitarioCoreValidator()
        {
            RuleFor(x => x.Valor)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}