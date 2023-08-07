using FluentValidation;

namespace Core.ValueObjects;

public sealed class Descricao : ValueObjectBase
{
    public Descricao(string descricao)
    {
        Value = string.IsNullOrWhiteSpace(descricao) ? descricao : descricao.Trim();
        Validate(this, new DescricaoValidator());

        if (!IsValid) Value = default;
    }

    protected Descricao()
    {
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is Descricao vO &&
               Value == vO.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public static bool operator ==(Descricao left, Descricao right) => left.Equals(right);

    public static bool operator !=(Descricao left, Descricao right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class DescricaoValidator : AbstractValidator<Descricao>
    {
        public DescricaoValidator()
        {
            RuleFor(x => x.Value)
                .NotNull()
                .MaximumLength(255);
        }
    }
}