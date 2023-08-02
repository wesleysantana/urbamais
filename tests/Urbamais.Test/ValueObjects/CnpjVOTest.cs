using Core.ValueObjects;

namespace Urbamais.Test.ValueObjects;

public class CnpjVOTest
{
    [Fact]
    public void CnpjAdequado()
    {
        var cnpj = new Cnpj("11.587.881/0001-05");

        Assert.True(cnpj.IsValid);
    }

    [Fact]
    public void CnpjIncorreto()
    {
        var cnpj = new Cnpj("11.587.881/0001-04");

        Assert.False(cnpj.IsValid);

        var msg = $"The specified condition was not met for '{nameof(cnpj.Value)}'.";
        Assert.Contains(cnpj.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CnpjTamanhoMenor()
    {
        var valor = "11587881";
        var cnpj = new Cnpj(valor);

        Assert.False(cnpj.IsValid);

        var msg = $"'{nameof(cnpj.Value)}' must be 14 characters in length. You entered {valor.Length} characters.";
        Assert.Contains(cnpj.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CnpjTamanhoMaior()
    {
        var valor = "1158788100010404";
        var cnpj = new Cnpj(valor);

        Assert.False(cnpj.IsValid);

        var msg = $"'{nameof(cnpj.Value)}' must be 14 characters in length. You entered {valor.Length} characters.";
        Assert.Contains(cnpj.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CnpjVazio()
    {
        var cnpj = new Cnpj("");

        Assert.False(cnpj.IsValid);

        var msg = $"'{nameof(cnpj.Value)}' must be 14 characters in length. You entered 0 characters.";
        Assert.Contains(cnpj.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var cnpj1 = new Cnpj("11.587.881/0001-05");
        var cnpj2 = new Cnpj("11.587.881/0001-05");

        Assert.Equal(cnpj1, cnpj2);
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var cnpj1 = new Cnpj("11.587.881/0001-05");
        var cnpj2 = new Cnpj("48.685.811/0001-08");

        Assert.False(cnpj1.Equals(cnpj2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var cnpj1 = new Cnpj("11.587.881/0001-05");
        var cnpj2 = new Cnpj("11.587.881/0001-05");

        Assert.True(cnpj1 == cnpj2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var cnpj1 = new Cnpj("11.587.881/0001-05");
        var cnpj2 = new Cnpj("48.685.811/0001-08");

        Assert.True(cnpj1 != cnpj2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var cnpj1 = new Cnpj("11.587.881/0001-05");
        var cnpj2 = new Cnpj("11.587.881/0001-05");

        Assert.True(cnpj1.GetHashCode().Equals(cnpj2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var cnpj1 = new Cnpj("11.587.881/0001-05");
        var cnpj2 = new Cnpj("48.685.811/0001-08");

        Assert.False(cnpj1.GetHashCode().Equals(cnpj2.GetHashCode()));
    }
}