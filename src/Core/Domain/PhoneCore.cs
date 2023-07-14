using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public class PhoneCore : BaseEntity, IEntity
{
    public string? Number { get; private set; }

    public PhoneCore(string number)
    {
        Number = number.Trim();
        Validate(this, new PhoneValidator());

        if (!IsValid && Id == default) Number = default;
    }

    #region Sobrescrita Object

    public override string ToString() => $"Telefone - Id: {Id}, Número: {Number}";

    public override bool Equals(object? obj)
    {
        return obj is PhoneCore telefone &&
               Number == telefone.Number;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Number);
    }

    #endregion Sobrescrita Object

    public static bool operator ==(PhoneCore left, PhoneCore right) => left.Equals(right);

    public static bool operator !=(PhoneCore left, PhoneCore right) => !left.Equals(right);

    private class PhoneValidator : AbstractValidator<PhoneCore>
    {
        public PhoneValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}