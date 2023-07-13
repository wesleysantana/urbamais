using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;

namespace Core.Domain;

public abstract class CityCore : BaseEntity, IAggregateRoot
{
    public NameVO Name { get; private set; }
    public Uf Uf { get; private set; }    

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected CityCore()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public CityCore(NameVO name, Uf uf)
    {
        Name = name;
        Uf = uf;
        CreationDate = DateTime.Now;        

        Validar();
    }

    private void Validar()
    {
        ValidationResult?.Errors.AddRange(Name.ValidationResult!.Errors);

        Validate(this, new CityValidator());

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    public void Update(NameVO? name = null, Uf? uf = null)
    {
        if (name is not null) Name = name;
        if (uf is not null) Uf = uf.Value;

        ModificationDate = DateTime.Now;
        Validar();
    }

    #region Sobrescrita Object

    public override string ToString() => $"Cidade - Id: {Id}, Nome: {Name}, Estado: {Uf}";

    public override bool Equals(object? obj)
    {
        return obj is CityCore cidade &&
            Id == cidade.Id &&
            EqualityComparer<NameVO>.Default.Equals(Name, cidade.Name) &&
            EqualityComparer<Uf>.Default.Equals(Uf, cidade.Uf);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Uf);
    }

    public static bool operator ==(CityCore left, CityCore right) => left.Equals(right);

    public static bool operator !=(CityCore left, CityCore right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class CityValidator : AbstractValidator<CityCore>
    {
        public CityValidator()
        {
            RuleFor(x => x.Uf)
                .IsInEnum();
        }
    }
}