using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using System.Reflection;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Domain.Entities.Obras;

public class Obra : BaseEntity, IAggregateRoot
{
    public Descricao Descricao { get; private set; }
    public virtual ICollection<Planejamento>? Planejamentos { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Obra()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Obra(string idUserCreation, Descricao descricao)
    {
        Descricao = descricao;

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
            IdUserCreation = idUserCreation;
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Descricao.ValidationResult!.Errors);
    }

    public void Update(string idUserModification, Descricao description)
    {
        var memento = CreateMemento();

        Descricao = description;
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
            Descricao
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Descricao = state.Descricao;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() =>
        $"Obra - Id: {Id}, Descrição: {Descricao}";

    public override bool Equals(object? obj)
    {
        return obj is Obra obra &&
            Id == obra.Id &&
            Descricao == obra.Descricao;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Descricao);
    }

    public static bool operator ==(Obra left, Obra right) => left.Equals(right);

    public static bool operator !=(Obra left, Obra right) => !left.Equals(right);

    #endregion Sobrescrita Object
}