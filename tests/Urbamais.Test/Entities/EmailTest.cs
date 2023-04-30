using Urbamais.Domain.Entities.Core;

namespace Urbamais.Test.Entities;

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
        Assert.Contains(email.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void EmailVazio()
    {
        var email = new Email("");

        Assert.False(email.IsValid);
        var msg = $"'{nameof(email.Endereco)}' is not a valid email address.";
        Assert.Contains(email.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var email1 = new Email("wel.santana@hotmail.com");
        var email2 = new Email("wel.santana@hotmail.com");

        Assert.True(email1.Equals(email2));
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var email1 = new Email("wel.santana@hotmail.com");
        var email2 = new Email("rita.santana@hotmail.com");

        Assert.False(email1.Equals(email2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var email1 = new Email("wel.santana@hotmail.com");
        var email2 = new Email("wel.santana@hotmail.com");

        Assert.True(email1 == email2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var email1 = new Email("wel.santana@hotmail.com");
        var email2 = new Email("rita.santana@hotmail.com");

        Assert.True(email1 != email2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var email1 = new Email("wel.santana@hotmail.com");
        var email2 = new Email("wel.santana@hotmail.com");

        Assert.True(email1.GetHashCode().Equals(email2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var email1 = new Email("wel.santana@hotmail.com");
        var email2 = new Email("rita.santana@hotmail.com");

        Assert.False(email1.GetHashCode().Equals(email2.GetHashCode()));
    }
}