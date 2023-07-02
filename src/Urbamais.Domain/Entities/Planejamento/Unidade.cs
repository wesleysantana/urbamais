using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Urbamais.Domain.Entities.Planejamento;

public class Unidade : BaseEntity, IAggregateRoot
{
    public string? Descricao { get; private set; }
    public string? Sigla { get; private set; }
    public virtual ICollection<Insumo>? Insumos { get; private set; }

    public Unidade(string? descricao, string? sigla)
    {
        Descricao = descricao?.Trim();
        Sigla = sigla?.Trim();

        Validar();
    }

    private void Validar()
    {
        Validate(this, new UnidadeValidator());

        if (!IsValid && Id == 0)
        {
            Descricao = default;
            Sigla = default;
        }
    }

    public void Update(string? descricao = null, string? sigla = null)
    {
        if (!string.IsNullOrWhiteSpace(descricao)) Descricao = descricao.Trim();
        if (!string.IsNullOrWhiteSpace(sigla)) Sigla = sigla.Trim();

        Validar();

        if(IsValid) DataAlteracao = DateTime.Now;
    }

    #region Sobrescrita Object

    public override string ToString() =>
        $"Unidade - Id: {Id}, Descricao: {Descricao}, Sigla: {Sigla}";

    public override bool Equals(object? obj)
    {
        return obj is Unidade unidade &&
            Id == unidade.Id &&
            Descricao == unidade.Descricao &&
            Sigla == unidade.Sigla;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Descricao, Sigla);
    }

    public static bool operator ==(Unidade left, Unidade right) => left.Equals(right);

    public static bool operator !=(Unidade left, Unidade right) => !left.Equals(right);

    #endregion Sobrescrita Object

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