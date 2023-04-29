using FluentValidation;
using Urbamais.Domain.SeedWork;

namespace Urbamais.Domain.ValueObjects;

public class NomeVO : BaseValidate
{
    public string? Nome { get; private set; }

    public NomeVO(string nome)
    {
        Nome = nome.Trim();
        Validate(this, new NomeValidator());

        if (!IsValid) Nome = default;
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is NomeVO vO &&
               Nome == vO.Nome;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Nome);
    }

    public static bool operator ==(NomeVO left, NomeVO right) => left.Equals(right);

    public static bool operator !=(NomeVO left, NomeVO right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class NomeValidator : AbstractValidator<NomeVO>
    {
        public NomeValidator()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(3)
                .MaximumLength(255);
        }
    }
}