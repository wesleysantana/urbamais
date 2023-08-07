using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using System.Reflection;

namespace Urbamais.Domain.Entities.Fornecedores;

public class Equipamento : BaseEntity, IAggregateRoot
{
    public Nome Nome { get; private set; }
    public Descricao Descricao { get; private set; }
    public ICollection<Fornecedor>? Fornecedores { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Equipamento()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Equipamento(string idUserCreation, Nome nome, Descricao descricao)
    {
        Nome = nome;
        Descricao = descricao;

        Validate();

        if (IsValid)
        {
            IdUserCreation = idUserCreation;
        }
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Nome.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Descricao.ValidationResult!.Errors);

        if (!IsValid && Id == default)
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in properties)
            {
                item.SetValue(this, default);
            }
        }
    }

    public void Update(string idUserModification, Nome? name = null, Descricao? description = null)
    {
        var memento = CreateMemento();

        if (name is not null) Nome = name;
        if (description is not null) Descricao = description;

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
            Descricao
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Nome = state.Name;
        Descricao = state.Description;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() => $"Equipamento - Id: {Id}, Nome: {Nome}, Descrição: {Descricao}";

    public override bool Equals(object? obj)
    {
        return obj is Equipamento equipamento &&
            Id == equipamento.Id &&
            EqualityComparer<Nome>.Default.Equals(Nome, equipamento.Nome) &&
            EqualityComparer<Descricao>.Default.Equals(Descricao, equipamento.Descricao);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nome, Descricao);
    }

    public static bool operator ==(Equipamento left, Equipamento right) => left.Equals(right);

    public static bool operator !=(Equipamento left, Equipamento right) => !left.Equals(right);

    #endregion Sobrescrita Object
}