using FluentValidation;

namespace Core.ValueObjects;

public sealed class Nome : ValueObjectBase
{
    public Nome(string nome)
    {
        Value = string.IsNullOrWhiteSpace(nome) ? nome : nome.Trim();
        Validate(this, new NomeValidator());

        if (!IsValid) Value = default;
    }

    protected Nome()
    {
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is Nome vO && Value == vO.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public static bool operator ==(Nome left, Nome right) => left.Equals(right);

    public static bool operator !=(Nome left, Nome right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class NomeValidator : AbstractValidator<Nome>
    {
        public NomeValidator()
        {
            RuleFor(x => x.Value)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(255);
        }
    }
}