using FluentValidation;
using Urbamais.Domain.SeedWork;

namespace Urbamais.Domain.ValueObjects;

public class DescricaoVO : BaseValidate
{
    public string? Descricao { get; private set; }

    public DescricaoVO(string descricao)
    {
        Descricao = descricao.Trim();
        Validate(this, new DescricaoValidator());

        if (!IsValid) Descricao = default;
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is DescricaoVO vO &&
               Descricao == vO.Descricao;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Descricao);
    }

    public static bool operator ==(DescricaoVO left, DescricaoVO right) => left.Equals(right);

    public static bool operator !=(DescricaoVO left, DescricaoVO right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class DescricaoValidator : AbstractValidator<DescricaoVO>
    {
        public DescricaoValidator()
        {
            RuleFor(x => x.Descricao)
                .MaximumLength(255);
        }
    }
}