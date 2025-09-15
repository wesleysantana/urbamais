using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Test.Domain;

public class EnderecoTest
{
    [Fact]
    public void EnderecoValido()
    {
        var endereco = new Endereco("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1);

        Assert.True(endereco.IsValid);
    }

    [Fact]
    public void LogradouroVazio()
    {
        var endereco = new Endereco("", "70", "", "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"'{nameof(endereco.Logradouro)}' must not be empty.";
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

        var endereco = new Endereco(logradouro, "70", "", "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"The length of '{nameof(endereco.Logradouro)}' must be 150 characters or fewer. " +
            $"You entered {logradouro.Length} characters.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void NumeroVazio()
    {
        var endereco = new Endereco("Rua Tito Lívio Brasil", "", "", "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"'{nameof(endereco.Numero)}' must not be empty.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void NumeroMaiorQueOLimiteDeCaracteres()
    {
        var numero = "012345678901";

        var endereco = new Endereco("Rua Tito Lívio Brasil", numero, "", "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"The length of '{nameof(endereco.Numero)}' must be 10 characters or fewer. " +
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

        var endereco = new Endereco("Rua Tito Lívio Brasil", "70", complemento, "Vila Verinha", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"The length of '{nameof(endereco.Complemento)}' must be 100 characters or fewer. " +
            $"You entered {complemento.Length} characters.";
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void BairroVazio()
    {
        var endereco = new Endereco("Rua Tito Lívio Brasil", "70", "", "", "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"'{nameof(endereco.Bairro)}' must not be empty.";
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

        var endereco = new Endereco("Rua Tito Lívio Brasil", "70", "", bairro, "19040170", 1);

        Assert.False(endereco.IsValid);

        var msg = $"The length of '{nameof(endereco.Bairro)}' must be 100 characters or fewer. " +
            $"You entered {bairro.Length} characters.";
        var end = endereco.ValidationResult!.Errors.First().ErrorMessage;
        Assert.Contains(endereco.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }    
}