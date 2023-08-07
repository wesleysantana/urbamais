using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;

namespace Core.Domain;

public abstract class CidadeCore : BaseEntity, IAggregateRoot
{
    public Nome Nome { get; private set; }
    public Uf Uf { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected CidadeCore()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public CidadeCore(string userId, Nome nome, Uf uf)
    {
        Nome = nome;
        Uf = uf;
        CreationDate = DateTime.Now;

        Validate();

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
        else
            IdUserCreation = userId;
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Nome.ValidationResult!.Errors);

        Validate(this, new CidadeValidator());        
    }

    public void Update(string idUserModification, Nome? name = null, Uf? uf = null)
    {
        var memento = CreateMemento();

        if (name is not null) Nome = name;
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
            Nome,
            Uf
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Nome = state.Name;
        Uf = state.Uf;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() => $"Cidade - Id: {Id}, Nome: {Nome}, Estado: {Uf}";

    public override bool Equals(object? obj)
    {
        return obj is CidadeCore cidade &&
            Id == cidade.Id &&
            EqualityComparer<Nome>.Default.Equals(Nome, cidade.Nome) &&
            EqualityComparer<Uf>.Default.Equals(Uf, cidade.Uf);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nome, Uf);
    }

    public static bool operator ==(CidadeCore left, CidadeCore right) => left.Equals(right);

    public static bool operator !=(CidadeCore left, CidadeCore right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class CidadeValidator : AbstractValidator<CidadeCore>
    {
        public CidadeValidator()
        {
            RuleFor(x => x.Uf)
                .IsInEnum();
        }
    }
}