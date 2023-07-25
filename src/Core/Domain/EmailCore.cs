using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public abstract class EmailCore : BaseEntity, IEntity
{
    public string? Address { get; private set; }

    public EmailCore(string idUserCreation, string address)
    {
        Address = address.Trim();

        Validate(this, new EmailValidator());

        if (!IsValid && Id == default)
            Address = default;
        else
            IdUserCreation = idUserCreation;
    }

    public void Update(string idUserModification, string address)
    {
        var memento = CreateMemento();

        Address = address.Trim();
        Validate(this, new EmailValidator());

        if (IsValid)
        {
            IdUserModification = idUserModification;
            ModificationDate = DateTime.Now;
        }
        else
            RestoreMemento(memento);
    }

    #region Memento

    private object CreateMemento()
    {
        return new
        {
            Address
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Address = state.Address;
    }

    #endregion Memento

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