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

    public CityCore(string userId, NameVO name, Uf uf)
    {
        Name = name;
        Uf = uf;
        CreationDate = DateTime.Now;

        Validate();

        if (IsValid)
            IdUserCreation = userId;
    }

    private void Validate()
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

    public void Update(string idUserModification, NameVO? name = null, Uf? uf = null)
    {
        var memento = CreateMemento();

        if (name is not null) Name = name;
        if (uf is not null) Uf = uf.Value;

        Validate();

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
            Name,
            Uf
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Name = state.Name;
        Uf = state.Uf;
    }

    #endregion Memento

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