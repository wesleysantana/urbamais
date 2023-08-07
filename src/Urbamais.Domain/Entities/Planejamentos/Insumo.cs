using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;

namespace Urbamais.Domain.Entities.Planejamentos;

public class Insumo : BaseEntity, IAggregateRoot
{
    public Nome Nome { get; private set; }
    public Descricao Descricao { get; private set; }
    public int UnidadeId { get; private set; }
    public virtual Unidade? Unidade { get; private set; }
    public TipoInsumo Tipo { get; private set; }
    public virtual ICollection<PlanejamentoInsumo>? PlanejamentosInsumos { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Insumo()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Insumo(string idUserCreation, Nome nome, Descricao descricao, int unidadeId, TipoInsumo tipo)
    {
        Nome = nome;
        Descricao = descricao;
        UnidadeId = unidadeId;
        Tipo = tipo;

        Validate();

        if (!IsValid)
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in properties)
                item.SetValue(this, default);
        }
        else
            IdUserCreation = idUserCreation;
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Nome.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Descricao.ValidationResult!.Errors);

        Validate(this, new InsumoValidator());
    }

    public void Update(string? idUserModification = null, string? nome = null,
        string? descricao = null, int? unidadeId = null, TipoInsumo? tipo = null)
    {
        var memento = CreateMemento();
        
        if (!string.IsNullOrWhiteSpace(nome)) Nome = new Nome(nome!);
        if (!string.IsNullOrWhiteSpace(descricao)) Descricao = new Descricao(descricao!);
        if (unidadeId is not null) UnidadeId = (int)unidadeId;
        if (tipo is not null) Tipo = tipo.Value;

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
            Nome,
            Descricao,
            UnidadeId,
            Tipo
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Nome = state.Nome;
        Descricao = state.Descricao;
        UnidadeId = state.UnidadeId;
        Tipo = state.Tipo;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() => $"Insumo - Id: {Id}, Nome: {Nome}, Descrição: {Descricao}, " +
        $"Unidade: {Unidade}, Tipo: {Tipo}";

    public override bool Equals(object? obj)
    {
        return obj is Insumo insumo &&
            EqualityComparer<Nome>.Default.Equals(Nome, insumo.Nome) &&
            Descricao == insumo.Descricao &&
            EqualityComparer<Unidade>.Default.Equals(Unidade, insumo.Unidade) &&
            Tipo == insumo.Tipo;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nome, Descricao, Unidade, Tipo);
    }

    public static bool operator ==(Insumo left, Insumo right) => left.Equals(right);

    public static bool operator !=(Insumo left, Insumo right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class InsumoValidator : AbstractValidator<Insumo>
    {
        public InsumoValidator()
        {
            RuleFor(x => x.Tipo)
                .IsInEnum();
        }
    }
}