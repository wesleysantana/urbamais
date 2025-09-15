using Core.ValueObjects;

namespace Urbamais.Test.ValueObjects;

public class NomeVOTest
{
    [Fact]
    public void NomeAdequado()
    {
        var nome = new NomeVO("Wesley Santana");

        Assert.True(nome.IsValid);
    }

    [Fact]
    public void NomeComMenosDe3Caracteres()
    {
        var valor = "12";
        var nome = new NomeVO(valor);

        Assert.False(nome.IsValid);

        var msg = $"The length of '{nameof(nome.Nome)}' must be at least 3 characters. You entered {valor.Length} characters.";
        Assert.Contains(nome.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void NomeVazio()
    {
        var nome = new NomeVO("");

        Assert.False(nome.IsValid);

        var msg = $"The length of '{nameof(nome.Nome)}' must be at least 3 characters. You entered 0 characters.";
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

        var nome = new NomeVO(valor);
        Assert.False(nome.IsValid);

        var msg = $"The length of '{nameof(nome.Nome)}' must be 255 characters or fewer. You entered {valor.Length} characters.";
        Assert.Contains(nome.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }   
}