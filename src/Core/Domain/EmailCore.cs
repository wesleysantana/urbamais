using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public abstract class EmailCore : BaseEntity, IEntity
{
    public string? Address { get; private set; }

    public EmailCore(string address)
    {
        Address = address.Trim();

        Validate(this, new EmailValidator());

        if (!IsValid && Id == default) Address = default;
    }

    #region Sobrescrita Object

    public override string ToString() => $"Email: {Address}";

    public override bool Equals(object? obj)
    {
        return obj is EmailCore email &&
            Id == email.Id &&
            Address == email.Address;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Address);
    }

    public static bool operator ==(EmailCore left, EmailCore right) => left.Equals(right);

    public static bool operator !=(EmailCore left, EmailCore right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class EmailValidator : AbstractValidator<EmailCore>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Address)
                .EmailAddress();

            RuleFor(x => x.Address)
                .MaximumLength(255);
        }
    }
}