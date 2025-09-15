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
        Validate(this, new CidadeValidator());
        AddErrorsFrom(Nome);

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var p in propriedades)
                p.SetValue(this, default);
        }
    }

    public void Update(NomeVO? nome = null, Uf? uf = null)
    {
        if (nome is not null) Nome = nome;
        if (uf is not null) Uf = uf.Value;

        DataAlteracao = DateTime.Now;
        Validar();
    }   

    private class CidadeValidator : AbstractValidator<CidadeCore>
    {
        public CidadeValidator()
        {
            RuleFor(x => x.Uf)
                .IsInEnum();
        }
    }
}