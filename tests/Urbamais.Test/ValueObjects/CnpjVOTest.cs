using Core.ValueObjects;

namespace Urbamais.Test.ValueObjects;

public class CnpjVOTest
{
    [Fact]
    public void CnpjAdequado()
    {
        var cnpj = new CnpjVO("11.587.881/0001-05");

        Assert.True(cnpj.IsValid);
    }

    [Fact]
    public void CnpjIncorreto()
    {
        var cnpj = new CnpjVO("11.587.881/0001-04");

        Assert.False(cnpj.IsValid);

        var msg = $"The specified condition was not met for '{nameof(cnpj.Cnpj)}'.";
        Assert.Contains(cnpj.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CnpjTamanhoMenor()
    {
        var valor = "11587881";
        var cnpj = new CnpjVO(valor);

        Assert.False(cnpj.IsValid);

        var msg = $"'{nameof(cnpj.Cnpj)}' must be 14 characters in length. You entered {valor.Length} characters.";
        Assert.Contains(cnpj.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CnpjTamanhoMaior()
    {
        var valor = "1158788100010404";
        var cnpj = new CnpjVO(valor);

        Assert.False(cnpj.IsValid);

        var msg = $"'{nameof(cnpj.Cnpj)}' must be 14 characters in length. You entered {valor.Length} characters.";
        Assert.Contains(cnpj.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CnpjVazio()
    {
        var cnpj = new CnpjVO("");

        Assert.False(cnpj.IsValid);

        var msg = $"'{nameof(cnpj.Cnpj)}' must be 14 characters in length. You entered 0 characters.";
        Assert.Contains(cnpj.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }    
}