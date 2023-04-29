using Urbamais.Domain.ValueObjects;

namespace Urbamais.Test.ValueObjects;

public class CpfVOTest
{
    [Fact]
    public void CpfAdequado()
    {
        var Cpf = new CpfVO("451.254.470-48");

        Assert.True(Cpf.IsValid);
    }

    [Fact]
    public void CpfIncorreto()
    {
        var cpf = new CpfVO("451.254.470-47");

        Assert.False(cpf.IsValid);

        var msg = $"The specified condition was not met for '{nameof(cpf.Cpf)}'.";
        Assert.Contains(cpf.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CpfTamanhoMenor()
    {
        var valor = "11587881";
        var cpf = new CpfVO(valor);

        Assert.False(cpf.IsValid);

        var msg = $"'{nameof(cpf.Cpf)}' must be 11 characters in length. You entered {valor.Length} characters.";
        Assert.Contains(cpf.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CpfTamanhoMaior()
    {
        var valor = "1158788100010404";
        var cpf = new CpfVO(valor);

        Assert.False(cpf.IsValid);

        var msg = $"'{nameof(cpf.Cpf)}' must be 11 characters in length. You entered {valor.Length} characters.";
        Assert.Contains(cpf.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CpfVazio()
    {
        var cpf = new CpfVO("");

        Assert.False(cpf.IsValid);

        var msg = $"'{nameof(cpf.Cpf)}' must be 11 characters in length. You entered 0 characters.";
        Assert.Contains(cpf.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var cpf1 = new CpfVO("451.254.470-48");
        var cpf2 = new CpfVO("451.254.470-48");

        Assert.Equal(cpf1, cpf2);
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var cpf1 = new CpfVO("451.254.470-48");
        var cpf2 = new CpfVO("044.194.180-01");

        Assert.False(cpf1.Equals(cpf2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var cpf1 = new CpfVO("451.254.470-48");
        var cpf2 = new CpfVO("451.254.470-48");

        Assert.True(cpf1 == cpf2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var cpf1 = new CpfVO("451.254.470-48");
        var cpf2 = new CpfVO("044.194.180-01");

        Assert.True(cpf1 != cpf2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var cpf1 = new CpfVO("451.254.470-48");
        var cpf2 = new CpfVO("451.254.470-48");

        Assert.True(cpf1.GetHashCode().Equals(cpf2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var cpf1 = new CpfVO("451.254.470-48");
        var cpf2 = new CpfVO("044.194.180-01");

        Assert.False(cpf1.GetHashCode().Equals(cpf2.GetHashCode()));
    }
}