using FluentValidation;

namespace Urbamais.Domain.Entities.Planejamento;

public class Unidade : BaseEntity, IAggregateRoot
{
    public string? Descricao { get; private set; }

    public Unidade(string? descricao)
    {
        Descricao = descricao?.Trim();

        Validate(this, new UnidadeValidator());

        if (!IsValid) Descricao = default;
    }

    #region Sobrescrita Object

    public override string ToString() =>
        $"Unidade - Id: {Id}, Descricao: {Descricao}";

    public override bool Equals(object? obj)
    {
        return obj is Unidade unidade &&
            Id == unidade.Id &&
            Descricao == unidade.Descricao;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Descricao);
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
        }
    }
}