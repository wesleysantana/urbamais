using FluentValidation;

namespace Urbamais.Domain.Entities.Core;

public sealed class Uf : BaseEntity, IAggregateRoot
{
    public string? Sigla { get; private set; }

    public Uf(string siglaUf)
    {
        Sigla = siglaUf.ToUpper().Trim();

        Validate(this, new UfValidator());

        if (!IsValid) Sigla = default;
    }

    #region Sobrescrita Object

    public override string ToString() => $"Uf - Id: {Id}, Sigla: {Sigla}";

    public override bool Equals(object? obj)
    {
        return obj is Uf uf &&
               Sigla == uf.Sigla;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Sigla);
    }

    public static bool operator ==(Uf left, Uf right) => left.Equals(right);

    public static bool operator !=(Uf left, Uf right) => !left.Equals(right);

    #endregion

    private class UfValidator : AbstractValidator<Uf>
    {
        public UfValidator()
        {
            RuleFor(x => x.Sigla)
                .Length(2)
                .Must(x => Validar(x));
        }

        private static bool Validar(string siglaUf)
        {
            if (siglaUf == ""
                || siglaUf == "AC"
                || siglaUf == "AL"
                || siglaUf == "AP"
                || siglaUf == "AM"
                || siglaUf == "BA"
                || siglaUf == "CE"
                || siglaUf == "DF"
                || siglaUf == "ES"
                || siglaUf == "GO"
                || siglaUf == "MA"
                || siglaUf == "MT"
                || siglaUf == "MS"
                || siglaUf == "MG"
                || siglaUf == "PA"
                || siglaUf == "PB"
                || siglaUf == "PR"
                || siglaUf == "PE"
                || siglaUf == "PI"
                || siglaUf == "RJ"
                || siglaUf == "RN"
                || siglaUf == "RS"
                || siglaUf == "RO"
                || siglaUf == "RR"
                || siglaUf == "SC"
                || siglaUf == "SP"
                || siglaUf == "SE"
                || siglaUf == "TO")
            {
                return true;
            }

            return false;
        }
    }
}