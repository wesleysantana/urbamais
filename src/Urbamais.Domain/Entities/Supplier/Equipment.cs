using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using System.Reflection;

namespace Urbamais.Domain.Entities.Supplier;

public class Equipment : BaseEntity, IAggregateRoot
{
    public NameVO Name { get; private set; }
    public DescriptionVO Description { get; private set; }
    public ICollection<Supplier>? Suppliers { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Equipment()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Equipment(NameVO name, DescriptionVO description)
    {
        Name = name;
        Description = description;

        Validate();
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Name.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Description.ValidationResult!.Errors);

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    public void Update(NameVO? name = null, DescriptionVO? description = null)
    {
        if (name is not null) Name = name;
        if (description is not null) Description = description;

        Validate();
    }

    #region Sobrescrita Object

    public override string ToString() => $"Equipamento - Id: {Id}, Nome: {Name}, Descrição: {Description}";

    public override bool Equals(object? obj)
    {
        return obj is Equipment equipamento &&
            Id == equipamento.Id &&
            EqualityComparer<NameVO>.Default.Equals(Name, equipamento.Name) &&
            EqualityComparer<DescriptionVO>.Default.Equals(Description, equipamento.Description);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description);
    }

    public static bool operator ==(Equipment left, Equipment right) => left.Equals(right);

    public static bool operator !=(Equipment left, Equipment right) => !left.Equals(right);

    #endregion Sobrescrita Object
}