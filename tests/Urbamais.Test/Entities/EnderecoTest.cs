using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Test.Entities;

public class EnderecoTest
{
    [Fact]
    public void EnderecoValido()
    {
        var endereco = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        Assert.True(endereco.IsValid);
    }

    [Fact]
    public void LogradouroVazio()
    {
        var endereco = new Address("", "70", "", "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"'{nameof(endereco.Thoroughfare)}' must not be empty.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void LogradouroMaiorQueOLimiteDeCaracteres()
    {
        var logradouro = "Rua Tito Lívio Brasil";
        while (logradouro.Length < 150)
        {
            logradouro += logradouro;
        }

        var endereco = new Address(logradouro, "70", "", "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"The length of '{nameof(endereco.Thoroughfare)}' must be 150 characters or fewer. " +
            $"You entered {logradouro.Length} characters.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void NumeroVazio()
    {
        var endereco = new Address("Rua Tito Lívio Brasil", "", "", "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"'{nameof(endereco.Number)}' must not be empty.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void NumeroMaiorQueOLimiteDeCaracteres()
    {
        var numero = "012345678901";

        var endereco = new Address("Rua Tito Lívio Brasil", numero, "", "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"The length of '{nameof(endereco.Number)}' must be 10 characters or fewer. " +
            $"You entered {numero.Length} characters.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void ComplementoMaiorQueOLimiteDeCaracteres()
    {
        var complemento = "complemento complemento";
        while (complemento.Length < 100)
        {
            complemento += complemento;
        }

        var endereco = new Address("Rua Tito Lívio Brasil", "70", complemento, "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"The length of '{nameof(endereco.Complement)}' must be 100 characters or fewer. " +
            $"You entered {complemento.Length} characters.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void BairroVazio()
    {
        var endereco = new Address("Rua Tito Lívio Brasil", "70", "", "", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"'{nameof(endereco.Neighborhood)}' must not be empty.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void BairroMaiorQueOLimiteDeCaracteres()
    {
        var bairro = "Vila Verinha";
        while (bairro.Length < 100)
        {
            bairro += bairro;
        }

        var endereco = new Address("Rua Tito Lívio Brasil", "70", "", bairro, "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"The length of '{nameof(endereco.Neighborhood)}' must be 100 characters or fewer. " +
            $"You entered {bairro.Length} characters.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var endereco1 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        var endereco2 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        Assert.True(endereco1.Equals(endereco2));
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var endereco1 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        var endereco2 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 2);

        Assert.False(endereco1.Equals(endereco2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var endereco1 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        var endereco2 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        Assert.True(endereco1 == endereco2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var endereco1 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        var endereco2 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 2);

        Assert.True(endereco1 != endereco2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var endereco1 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        var endereco2 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        Assert.True(endereco1.GetHashCode().Equals(endereco2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var endereco1 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        var endereco2 = new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 2);

        Assert.False(endereco1.GetHashCode().Equals(endereco2.GetHashCode()));
    }
}