using Core.SeedWork;
using FluentValidation;

namespace Core.ValueObjects;

public sealed class NameVO : BaseValidate
{
    public string? Name { get; private set; }

    public NameVO(string name)
    {
        Name = string.IsNullOrWhiteSpace(name) ? name : name.Trim();
        Validate(this, new NomeValidator());

        if (!IsValid) Name = default;
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is NameVO vO && Name == vO.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }

    public static bool operator ==(NameVO left, NameVO right) => left.Equals(right);

    public static bool operator !=(NameVO left, NameVO right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class NomeValidator : AbstractValidator<NameVO>
    {
        public NomeValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(255);
        }
    }
}