using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;

namespace Core.Domain;

public abstract class CidadeCore : BaseEntity, IAggregateRoot
{
    public NomeVO Nome { get; private set; }
    public Uf Uf { get; private set; }    

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected CidadeCore()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public CidadeCore(NomeVO nome, Uf uf)
    {
        Nome = nome;
        Uf = uf;
        DataCriacao = DateTime.Now;        

        Validar();
    }

    private void Validar()
    {
        ValidationResult?.Errors.AddRange(Nome.ValidationResult!.Errors);

        Validate(this, new CidadeValidator());

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    public void Update(NomeVO? nome = null, Uf? uf = null)
    {
        if (nome is not null) Nome = nome;
        if (uf is not null) Uf = uf.Value;

        DataAlteracao = DateTime.Now;
        Validar();
    }

    #region Sobrescrita Object

    public override string ToString() => $"Cidade - Id: {Id}, Nome: {Nome}, Estado: {Uf}";

    public override bool Equals(object? obj)
    {
        return obj is CidadeCore cidade &&
            Id == cidade.Id &&
            EqualityComparer<NomeVO>.Default.Equals(Nome, cidade.Nome) &&
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