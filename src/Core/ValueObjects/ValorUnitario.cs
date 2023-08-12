using FluentValidation;

namespace Core.ValueObjects;

public class ValorUnitario : ValueObjectBase
{
    public decimal Valor { get; private set; }

    public ValorUnitario(decimal valor)
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

    #endregion Memento

    private class ValorUnitarioCoreValidator : AbstractValidator<ValorUnitario>
    {
        public ValorUnitarioCoreValidator()
        {
            RuleFor(x => x.Valor)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}