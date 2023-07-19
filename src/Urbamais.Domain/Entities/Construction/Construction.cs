using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using System.Reflection;

namespace Urbamais.Domain.Entities.Construction;

public class Construction : BaseEntity, IAggregateRoot
{
    public int CompanieId { get; private set; }
    public virtual Companie Companie { get; private set; }
    public DescriptionVO Description { get; private set; }
    public virtual ICollection<Planning.Planning>? Plannings { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Construction()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Construction(string idUserCreation, Companie companie, DescriptionVO description)
    {
        Companie = companie;
        Description = description;

        Validate();

        if (IsValid)
        {
            IdUserCreation = idUserCreation;
        }
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Companie.ValidationResult!.Errors);

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    public void Update(string idUserModification, DescriptionVO description)
    {
        Description = description;
        Validate();

        if (IsValid)
        {
            IdUserModification = idUserModification;
            ModificationDate = DateTime.Now;
        }
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is Construction obra &&
            Id == obra.Id &&
            Description == obra.Description &&
            CompanieId == obra.CompanieId &&
            EqualityComparer<Companie>.Default.Equals(Companie, obra.Companie);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Description, CompanieId, Companie);
    }

    public static bool operator ==(Construction left, Construction right) => left.Equals(right);

    public static bool operator !=(Construction left, Construction right) => !left.Equals(right);

    #endregion Sobrescrita Object
}