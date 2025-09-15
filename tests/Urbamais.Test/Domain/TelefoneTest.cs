using Core.Domain;

namespace Urbamais.Test.Domain;

public class TelefoneTest
{
    [Fact]
    public void TelefoneValido()
    {
        var tel = new TelefoneCore("(18) 9 9714-4995");

        Assert.True(tel.IsValid);
    }

    [Fact]
    public void TelefoneVazio()
    {
        var tel = new TelefoneCore("");

        Assert.False(tel.IsValid);

        var msg = $"'{nameof(tel.Numero)}' must not be empty.";
        Assert.Contains(tel.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Ultrapassando20Caracters()
    {
        var valor = "12345678901234567890123";
        var tel = new TelefoneCore(valor);

        Assert.False(tel.IsValid);

        var msg = $"The length of '{nameof(tel.Numero)}' must be 20 characters or fewer. " +
            $"You entered {valor.Length} characters.";
        Assert.Contains(tel.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }    
}