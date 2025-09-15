using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;
using System.Reflection;

namespace Urbamais.Domain.Entities.Planejamento;

public class Unidade : BaseEntity, IAggregateRoot
{
    public string Descricao { get; private set; }
    public string Sigla { get; private set; }
    public virtual ICollection<Insumo>? Insumos { get; private set; }

    public Unidade(string descricao, string sigla)
    {
        Descricao = descricao.Trim();
        Sigla = sigla.Trim();

        Validar();
    }

    private void Validar()
    {
        Validate(this, new UnidadeValidator());       

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var p in propriedades)
                p.SetValue(this, default);
        }
    }

    public void Update(string? descricao = null, string? sigla = null)
    {
        if (!string.IsNullOrWhiteSpace(descricao)) Descricao = descricao.Trim();
        if (!string.IsNullOrWhiteSpace(sigla)) Sigla = sigla.Trim();

        Validar();

        if(IsValid) DataAlteracao = DateTime.Now;
    }   

    private class UnidadeValidator : AbstractValidator<Unidade>
    {
        public UnidadeValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Sigla)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}