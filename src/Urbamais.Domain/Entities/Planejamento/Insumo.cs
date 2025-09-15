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
    public virtual ICollection<PlanejamentoInsumo>? PlanejamentosInsumos { get; private set; }

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

        //ValidationResult?.Errors.AddRange(Nome.ValidationResult!.Errors);
        //ValidationResult?.Errors.AddRange(Unidade.ValidationResult!.Errors);

        //Validate(this, new InsumoValidator());

        //if (!IsValid && Id == default)
        //{
        //    var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        //    foreach (var item in propriedades)
        //        item.SetValue(this, default);
        //}

        Validate(this, new InsumoValidator());
        AddErrorsFrom(Nome);
        AddErrorsFrom(Unidade);

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var p in propriedades)
                p.SetValue(this, default);
        }
    }

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