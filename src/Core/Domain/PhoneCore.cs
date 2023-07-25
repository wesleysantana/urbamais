using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public class PhoneCore : BaseEntity, IEntity
{
    public string? Number { get; private set; }

    public PhoneCore(string idUserCreation, string number)
    {
        Number = number.Trim();
        Validate(this, new PhoneValidator());

        if (!IsValid)
            Number = default;
        else
            IdUserCreation = idUserCreation;
    }

    public void Update(string idUserModification, string number)
    {
        var memento = CreateMemento();

        Number = number.Trim();

        Validate(this, new PhoneValidator());

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
            Number
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Number = state.Number;
    }

    #endregion Memento

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