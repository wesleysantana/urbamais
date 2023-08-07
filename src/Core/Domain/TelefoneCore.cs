using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public class TelefoneCore : BaseEntity, IEntity
{
    public string? Numero { get; private set; }

    public TelefoneCore(string idUserCreation, string numero)
    {
        Numero = numero.Trim();
        Validate(this, new TelefoneValidator());

        if (!IsValid)
            Numero = default;
        else
            IdUserCreation = idUserCreation;
    }

    public void Update(string idUserModification, string numero)
    {
        var memento = CreateMemento();

        Numero = numero.Trim();

        Validate(this, new TelefoneValidator());

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
            Numero
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Numero = state.Number;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() => $"Telefone - Id: {Id}, Número: {Numero}";

    public override bool Equals(object? obj)
    {
        return obj is TelefoneCore telefone &&
               Numero == telefone.Numero;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Numero);
    }

    #endregion Sobrescrita Object

    public static bool operator ==(TelefoneCore left, TelefoneCore right) => left.Equals(right);

    public static bool operator !=(TelefoneCore left, TelefoneCore right) => !left.Equals(right);

    private class TelefoneValidator : AbstractValidator<TelefoneCore>
    {
        public TelefoneValidator()
        {
            RuleFor(x => x.Numero)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}