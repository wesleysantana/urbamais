using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;

namespace Urbamais.Domain.Entities.Planejamento;

public class Insumo : BaseEntity, IAggregateRoot
{
    public NomeVO Nome { get; private set; }
    public string Descricao { get; private set; }
    public int UnidadeId { get; private set; }
    public virtual Unidade Unidade { get; private set; }
    public TipoInsumo Tipo { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Insumo()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Insumo(NomeVO nome, string descricao, Unidade unidade, TipoInsumo tipo)
    {
        Nome = nome;
        Descricao = descricao.Trim();
        Unidade = unidade;
        Tipo = tipo;

        ValidationResult.Errors.AddRange(Nome.ValidationResult.Errors);
        ValidationResult.Errors.AddRange(Unidade.ValidationResult.Errors);

        Validate(this, new InsumoValidator());

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
                item.SetValue(this, default);
        }
    }

    #region Sobrescrita Object

    public override string ToString() => $"Insumo - Id: {Id}, Nome: {Nome}, Descrição: {Descricao}, " +
        $"Unidade: {Unidade}, Tipo: {Tipo}";

    public override bool Equals(object? obj)
    {
        return obj is Insumo insumo &&
            EqualityComparer<NomeVO>.Default.Equals(Nome, insumo.Nome) &&
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
            RuleFor(x => x.Descricao)
                .MaximumLength(255);

            RuleFor(x => x.Tipo)
                .IsInEnum();
        }
    }
}