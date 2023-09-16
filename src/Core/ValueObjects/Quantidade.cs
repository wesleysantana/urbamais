using Core.ValueObjects.Base;
using FluentValidation;

namespace Core.ValueObjects;

public class Quantidade : ValueObjectDouble
{
    public Quantidade(double valor)
    {
        Value = valor;

        Validate(this, new QuantidadeValidator());

        if (!IsValid)
            Value = default;
    }

    protected Quantidade()
    {        
    }

    public void Update(double valor)
    {
        var memento = CreateMemento();

        Value = valor;

        Validate(this, new QuantidadeValidator());

        if (!IsValid)
            RestoreMemento(memento);
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is Quantidade vO &&
               Value == vO.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public static bool operator ==(Quantidade left, Quantidade right) => left.Equals(right);

    public static bool operator !=(Quantidade left, Quantidade right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class QuantidadeValidator : AbstractValidator<Quantidade>
    {
        public QuantidadeValidator()
        {
            RuleFor(x => x.Value)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}