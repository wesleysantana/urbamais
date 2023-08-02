using FluentValidation;

namespace Core.ValueObjects;

public sealed class Cnpj : ValueObjectBase
{
    public Cnpj(string cnpj)
    {
        Value = string.IsNullOrWhiteSpace(cnpj) ? cnpj : cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
        Validate(this, new CnpjValidator());

        if (!IsValid) Value = default;
    }

    protected Cnpj()
    {
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is Cnpj vO &&
               Value == vO.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public static bool operator ==(Cnpj left, Cnpj right) => left.Equals(right);

    public static bool operator !=(Cnpj left, Cnpj right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class CnpjValidator : AbstractValidator<Cnpj>
    {
        public CnpjValidator()
        {
            RuleFor(x => x.Value)
                .NotNull()
                .Length(14)
                .Must(x => Validar(x));
        }

        private static bool Validar(string cnpj)
        {
            int[] digitos, soma, resultado;

            int nrDig;

            string ftmt = "65432987654321";

            bool[] CNPJOk;

            digitos = new int[14];

            soma = new int[2];

            soma[0] = 0;

            soma[1] = 0;

            resultado = new int[2];

            resultado[0] = 0;

            resultado[1] = 0;

            CNPJOk = new bool[2];

            CNPJOk[0] = false;

            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(cnpj.Substring(nrDig, 1));

                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }

                if (!(CNPJOk[0] && CNPJOk[1]))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}