namespace Core.Domain;

public enum Uf
{
    AC = 1,
    AL = 2,
    BA = 3,
    CE = 4,
    DF = 5,
    ES = 6,
    GO = 7,
    MA = 8,
    MT = 9,
    MS = 10,
    MG = 11,
    PA = 12,
    PB = 13,
    PR = 14,
    PE = 15,
    PI = 16,
    RJ = 17,
    RN = 18,
    RS = 19,
    RO = 20,
    RR = 21,
    SC = 22,
    SP = 23,
    SE = 24,
    TO = 25
}

//public sealed class Uf : IAggregateRoot
//{
//    public int Id { get; private set; }
//    public string? Sigla { get; private set; }
//    public List<Cidade>? Cidades { get; private set; }

//    public Uf(string sigla)
//    {
//        Sigla = sigla.Trim();
//    }

//    #region Sobrescrita Object

//    public override string ToString() => $"Uf - Id: {Id}, Sigla: {Sigla}";

//    public override bool Equals(object? obj)
//    {
//        return obj is Uf uf && Sigla == uf.Sigla;
//    }

//    public override int GetHashCode()
//    {
//        return HashCode.Combine(Id, Sigla);
//    }

//    public static bool operator ==(Uf left, Uf right) => left.Equals(right);

//    public static bool operator !=(Uf left, Uf right) => !left.Equals(right);

//    #endregion Sobrescrita Object

//    private class UfValidator : AbstractValidator<Uf>
//    {
//        public UfValidator()
//        {
//            RuleFor(x => x.Sigla)
//                .Length(2)
//                .Must(x => Validar(x));
//        }

//        private static bool Validar(string siglaUf)
//        {
//            if (siglaUf == ""
//                || siglaUf == "AC"
//                || siglaUf == "AL"
//                || siglaUf == "AP"
//                || siglaUf == "AM"
//                || siglaUf == "BA"
//                || siglaUf == "CE"
//                || siglaUf == "DF"
//                || siglaUf == "ES"
//                || siglaUf == "GO"
//                || siglaUf == "MA"
//                || siglaUf == "MT"
//                || siglaUf == "MS"
//                || siglaUf == "MG"
//                || siglaUf == "PA"
//                || siglaUf == "PB"
//                || siglaUf == "PR"
//                || siglaUf == "PE"
//                || siglaUf == "PI"
//                || siglaUf == "RJ"
//                || siglaUf == "RN"
//                || siglaUf == "RS"
//                || siglaUf == "RO"
//                || siglaUf == "RR"
//                || siglaUf == "SC"
//                || siglaUf == "SP"
//                || siglaUf == "SE"
//                || siglaUf == "TO")
//            {
//                return true;
//            }

//            return false;
//        }
//    }
//}