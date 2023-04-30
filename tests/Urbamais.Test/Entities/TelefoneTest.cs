using Urbamais.Domain.Entities.Core;

namespace Urbamais.Test.Entities;

public class TelefoneTest
{
    [Fact]
    public void TelefoneValido()
    {
        var tel = new Telefone("(18) 9 9714-4995");

        Assert.True(tel.IsValid);
    }

    [Fact]
    public void TelefoneVazio()
    {
        var tel = new Telefone("");

        Assert.False(tel.IsValid);

        var msg = $"'{nameof(tel.Numero)}' must not be empty.";
        Assert.Contains(tel.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Ultrapassando20Caracters()
    {
        var valor = "12345678901234567890123";
        var tel = new Telefone(valor);

        Assert.False(tel.IsValid);

        var msg = $"The length of '{nameof(tel.Numero)}' must be 20 characters or fewer. " +
            $"You entered {valor.Length} characters.";
        Assert.Contains(tel.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var telefone1 = new Telefone("(18) 9 9714-4995");
        var telefone2 = new Telefone("(18) 9 9714-4995");

        Assert.True(telefone1.Equals(telefone2));
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var telefone1 = new Telefone("(18) 9 9714-4995");
        var telefone2 = new Telefone("(18) 9 9714-4996");

        Assert.False(telefone1.Equals(telefone2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var telefone1 = new Telefone("(18) 9 9714-4995");
        var telefone2 = new Telefone("(18) 9 9714-4995");

        Assert.True(telefone1 == telefone2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var telefone1 = new Telefone("(18) 9 9714-4995");
        var telefone2 = new Telefone("(18) 9 9714-4996");

        Assert.True(telefone1 != telefone2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var telefone1 = new Telefone("(18) 9 9714-4995");
        var telefone2 = new Telefone("(18) 9 9714-4995");

        Assert.True(telefone1.GetHashCode().Equals(telefone2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var telefone1 = new Telefone("(18) 9 9714-4995");
        var telefone2 = new Telefone("(18) 9 9714-4996");

        Assert.False(telefone1.GetHashCode().Equals(telefone2.GetHashCode()));
    }
}