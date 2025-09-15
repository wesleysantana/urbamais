using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Test.Domain;

public class EmailTest
{
    [Fact]
    public void EmailValido()
    {
        var email = new Email("wel.santana@hotmail.com");

        Assert.True(email.IsValid);
    }

    [Fact]
    public void EmailInvalido()
    {
        var email = new Email("wel.santana_hotmail.com");

        Assert.False(email.IsValid);
        var msg = $"'{nameof(email.Endereco)}' is not a valid email address.";
        Assert.Contains(email.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void EmailVazio()
    {
        var email = new Email("");

        Assert.False(email.IsValid);
        var msg = $"'{nameof(email.Endereco)}' is not a valid email address.";
        Assert.Contains(email.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }   
}