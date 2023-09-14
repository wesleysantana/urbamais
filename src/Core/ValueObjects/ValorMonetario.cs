using Core.ValueObjects.Base;
using FluentValidation;

namespace Core.ValueObjects;

public class ValorMonetario : ValueObjectDecimal
{
    public ValorMonetario(decimal valor)
    {
        Value = valor;

        Validate(this, new ValorMonetarioValidator());

        if (!IsValid)
            Value = default;
    }

    public void Update(decimal valor)
    {
        var memento = CreateMemento();

        Value = valor;

        Validate(this, new ValorMonetarioValidator());

        if (!IsValid)
            RestoreMemento(memento);
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is ValorMonetario vO &&
               Value == vO.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public static bool operator ==(ValorMonetario left, ValorMonetario right) => left.Equals(right);

    public static bool operator !=(ValorMonetario left, ValorMonetario right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class ValorMonetarioValidator : AbstractValidator<ValorMonetario>
    {
        public ValorMonetarioValidator()
        {
            RuleFor(x => x.Value)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}