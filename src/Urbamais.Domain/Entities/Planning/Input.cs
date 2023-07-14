using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;

namespace Urbamais.Domain.Entities.Planning;

public class Input : BaseEntity, IAggregateRoot
{
    public NameVO Name { get; private set; }
    public string Description { get; private set; }
    public int UnitId { get; private set; }
    public virtual Unit Unit { get; private set; }
    public TipoInsumo Type { get; private set; }
    public virtual ICollection<PlanningInput>? PlannigInputs { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Input()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Input(NameVO name, string description, Unit unit, TipoInsumo type)
    {
        Name = name;
        Description = description.Trim();
        Unit = unit;
        Type = type;

        ValidationResult?.Errors.AddRange(Name.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Unit.ValidationResult!.Errors);

        Validate(this, new InputValidator());

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
                item.SetValue(this, default);
        }
    }

    #region Sobrescrita Object

    public override string ToString() => $"Insumo - Id: {Id}, Nome: {Name}, Descrição: {Description}, " +
        $"Unidade: {Unit}, Tipo: {Type}";

    public override bool Equals(object? obj)
    {
        return obj is Input insumo &&
            EqualityComparer<NameVO>.Default.Equals(Name, insumo.Name) &&
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
            RuleFor(x => x.Description)
                .MaximumLength(255);

            RuleFor(x => x.Type)
                .IsInEnum();
        }
    }
}