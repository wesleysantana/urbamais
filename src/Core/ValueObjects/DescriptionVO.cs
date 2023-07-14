using Core.SeedWork;
using FluentValidation;

namespace Core.ValueObjects;

public sealed class DescriptionVO : BaseValidate
{
    public string? Description { get; private set; }

    public DescriptionVO(string description)
    {
        Description = description.Trim();
        Validate(this, new DescriptionValidator());

        if (!IsValid) Description = default;
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is DescriptionVO vO &&
               Description == vO.Description;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Description);
    }

    public static bool operator ==(DescriptionVO left, DescriptionVO right) => left.Equals(right);

    public static bool operator !=(DescriptionVO left, DescriptionVO right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class DescriptionValidator : AbstractValidator<DescriptionVO>
    {
        public DescriptionValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(255);
        }
    }
}