using FluentValidation;

namespace Core.ValueObjects;

public sealed class Name : ValueObjectBase
{
    public Name(string name)
    {
        Value = string.IsNullOrWhiteSpace(name) ? name : name.Trim();
        Validate(this, new NameValidator());

        if (!IsValid) Value = default;
    }

    protected Name()
    {
    }

    public string ToUpper()
    {
        return Value!.ToUpper();
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is Name vO && Value == vO.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public static bool operator ==(Name left, Name right) => left.Equals(right);

    public static bool operator !=(Name left, Name right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(x => x.Value)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(255);
        }
    }
}