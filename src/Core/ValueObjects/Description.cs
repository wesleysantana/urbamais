using FluentValidation;

namespace Core.ValueObjects;

public sealed class Description : ValueObjectBase
{
    public Description(string description)
    {
        Value = string.IsNullOrWhiteSpace(description) ? description : description.Trim();
        Validate(this, new DescriptionValidator());

        if (!IsValid) Value = default;
    }

    protected Description()
    {
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is Description vO &&
               Value == vO.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public static bool operator ==(Description left, Description right) => left.Equals(right);

    public static bool operator !=(Description left, Description right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class DescriptionValidator : AbstractValidator<Description>
    {
        public DescriptionValidator()
        {
            RuleFor(x => x.Value)
                .NotNull()
                .MaximumLength(255);
        }
    }
}