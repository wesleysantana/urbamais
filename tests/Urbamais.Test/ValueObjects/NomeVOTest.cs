using Core.ValueObjects;

namespace Urbamais.Test.ValueObjects;

public class NomeVOTest
{
    [Fact]
    public void NomeAdequado()
    {
        var nome = new NameVO("Wesley Santana");

        Assert.True(nome.IsValid);
    }

    [Fact]
    public void NomeComMenosDe3Caracteres()
    {
        var valor = "12";
        var nome = new NameVO(valor);

        Assert.False(nome.IsValid);

        var msg = $"The length of '{nameof(nome.Name)}' must be at least 3 characters. You entered {valor.Length} characters.";
        Assert.Contains(nome.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void NomeVazio()
    {
        var nome = new NameVO("");

        Assert.False(nome.IsValid);

        var msg = $"The length of '{nameof(nome.Name)}' must be at least 3 characters. You entered 0 characters.";
        Assert.Contains(nome.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void NomeComMaisDe255Caracteres()
    {
        var valor = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
        Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an
        unknown printer took a galley of type and scrambled it to make a type specimen book.
        It has survived not only five centuries, but also the leap into electronic typesetting,
        remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset
        sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like
        Aldus PageMaker including versions of Lorem Ipsum.";

        var nome = new NameVO(valor);
        Assert.False(nome.IsValid);

        var msg = $"The length of '{nameof(nome.Name)}' must be 255 characters or fewer. You entered {valor.Length} characters.";
        Assert.Contains(nome.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var nome1 = new NameVO("Wesley Santana");
        var nome2 = new NameVO("Wesley Santana");

        Assert.Equal(nome1, nome2);
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var nome01 = new NameVO("Wesley Santana");
        var nome02 = new NameVO("Rita Santana");

        Assert.False(nome01.Equals(nome02));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var nome01 = new NameVO("Wesley Santana");
        var nome02 = new NameVO("Wesley Santana");

        Assert.True(nome01 == nome02);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var nome01 = new NameVO("Wesley Santana");
        var nome02 = new NameVO("Rita Santana");

        Assert.True(nome01 != nome02);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var nome01 = new NameVO("Wesley Santana");
        var nome02 = new NameVO("Wesley Santana");

        Assert.True(nome01.GetHashCode().Equals(nome02.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var nome01 = new NameVO("Wesley Santana");
        var nome02 = new NameVO("Rita Santana");

        Assert.False(nome01.GetHashCode().Equals(nome02.GetHashCode()));
    }
}