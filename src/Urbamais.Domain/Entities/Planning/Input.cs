using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;

namespace Urbamais.Domain.Entities.Planning;

public class Input : BaseEntity, IAggregateRoot
{
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public int UnitId { get; private set; }
    public virtual Unit? Unit { get; private set; }
    public InputType Type { get; private set; }
    public virtual ICollection<PlanningInput>? PlannigInputs { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Input()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Input(string idUserCreation, Name name, Description description, int unitId, InputType type)
    {
        Name = name;
        Description = description;
        UnitId = unitId;
        Type = type;

        Validate();

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
                item.SetValue(this, default);
        }
        else
            IdUserCreation = idUserCreation;
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Name.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Description.ValidationResult!.Errors);

        Validate(this, new InputValidator());
    }

    public void Update(string? idUserModification = null, string? name = null,
        string? description = null, int? unitId = null, InputType? type = null)
    {
        var memento = CreateMemento();
        
        if (!string.IsNullOrWhiteSpace(name)) Name = new Name(name!);
        if (!string.IsNullOrWhiteSpace(description)) Description = new Description(description!);
        if (unitId is not null) UnitId = (int)unitId;
        if (type is not null) Type = type.Value;

        Validate();

        if (IsValid)
        {
            IdUserModification = idUserModification;
            ModificationDate = DateTime.UtcNow;
        }
        else
            RestoreMemento(memento);
    }

    #region Memento

    private object CreateMemento()
    {
        return new
        {
            Name,
            Description,
            UnitId,
            Type
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Name = state.Name;
        Description = state.Description;
        UnitId = state.UnitId;
        Type = state.Type;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() => $"Insumo - Id: {Id}, Nome: {Name}, Descrição: {Description}, " +
        $"Unidade: {Unit}, Tipo: {Type}";

    public override bool Equals(object? obj)
    {
        return obj is Input insumo &&
            EqualityComparer<Name>.Default.Equals(Name, insumo.Name) &&
            Description == insumo.Description &&
            EqualityComparer<Unit>.Default.Equals(Unit, insumo.Unit) &&
            Type == insumo.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, Unit, Type);
    }

    public static bool operator ==(Input left, Input right) => left.Equals(right);

    public static bool operator !=(Input left, Input right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class InputValidator : AbstractValidator<Input>
    {
        public InputValidator()
        {
            RuleFor(x => x.Type)
                .IsInEnum();
        }
    }
}