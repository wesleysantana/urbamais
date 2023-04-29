using FluentValidation;
using Urbamais.Domain.SeedWork;

namespace Urbamais.Domain.ValueObjects;

public class CnpjVO : BaseValidate
{
    public string? Cnpj { get; private set; }

    public CnpjVO(string cnpj)
    {
        Cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
        Validate(this, new CnpjValidator());

        if (!IsValid) Cnpj = default;
    }

    #region Sobrescrita Object    

    public override bool Equals(object? obj)
    {
        return obj is CnpjVO vO &&
               Cnpj == vO.Cnpj;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Cnpj);
    }

    public static bool operator ==(CnpjVO left, CnpjVO right) => left.Equals(right);

    public static bool operator !=(CnpjVO left, CnpjVO right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class CnpjValidator : AbstractValidator<CnpjVO>
    {
        public CnpjValidator()
        {
            RuleFor(x => x.Cnpj)
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