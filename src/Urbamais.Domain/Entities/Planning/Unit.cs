using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Urbamais.Domain.Entities.Planning;

public class Unit : BaseEntity, IAggregateRoot
{
    public string Description { get; private set; }
    public string Acronym { get; private set; }
    public virtual ICollection<Input>? Inputs { get; private set; }

    public Unit(string idUserCreation, string description, string acronym)
    {
        Description = description.Trim();
        Acronym = acronym.Trim();

        Validate(this, new UnitValidator());

        if (!IsValid)
        {
            Description = default!;
            Acronym = default!;
        }
        else
            IdUserCreation = idUserCreation;
    }

    public void Update(string idUserModification, string? description = null, string? acronym = null)
    {
        var memento = CreateMemento();

        if (!string.IsNullOrWhiteSpace(description)) Description = description.Trim();
        if (!string.IsNullOrWhiteSpace(acronym)) Acronym = acronym.Trim();

        Validate(this, new UnitValidator());

        if (IsValid)
        {
            ModificationDate = DateTime.Now;
            IdUserModification = idUserModification;
        }
        else
            RestoreMemento(memento);
    }

    #region Memento

    private object CreateMemento()
    {
        return new
        {
            Description,
            Acronym
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Description = state.Description;
        Acronym = state.Acronym;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() =>
        $"Unidade - Id: {Id}, Descricao: {Description}, Sigla: {Acronym}";

    public override bool Equals(object? obj)
    {
        return obj is Unit unidade &&
            Id == unidade.Id &&
            Description == unidade.Description &&
            Acronym == unidade.Acronym;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Description, Acronym);
    }

    public static bool operator ==(Unit left, Unit right) => left.Equals(right);

    public static bool operator !=(Unit left, Unit right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class UnitValidator : AbstractValidator<Unit>
    {
        public UnitValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Acronym)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}