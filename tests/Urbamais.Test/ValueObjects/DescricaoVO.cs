using Urbamais.Domain.SeedWork;

namespace Urbamais.Test.ValueObjects;

public class DescricaoVO : BaseValidate
{
    [Fact]
    public void DescricaoAdequado()
    {
        var descricao = new Domain.ValueObjects.DescricaoVO("Descricao correta");

        Assert.True(descricao.IsValid);
    }

    [Fact]
    public void DescricaoComMaisDe255Caracteres()
    {
        var valor = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
        Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an
        unknown printer took a galley of type and scrambled it to make a type specimen book.
        It has survived not only five centuries, but also the leap into electronic typesetting,
        remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset
        sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like
        Aldus PageMaker including versions of Lorem Ipsum.";

        var Descricao = new Domain.ValueObjects.DescricaoVO(valor);
        Assert.False(Descricao.IsValid);

        var msg = $"The length of '{nameof(Descricao.Descricao)}' must be 255 characters or fewer. You entered {valor.Length} characters.";
        Assert.Contains(Descricao.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var Descricao1 = new Domain.ValueObjects.DescricaoVO("Wesley Santana");
        var Descricao2 = new Domain.ValueObjects.DescricaoVO("Wesley Santana");

        Assert.Equal(Descricao1, Descricao2);
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var Descricao01 = new Domain.ValueObjects.DescricaoVO("Wesley Santana");
        var Descricao02 = new Domain.ValueObjects.DescricaoVO("Rita Santana");

        Assert.False(Descricao01.Equals(Descricao02));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var Descricao01 = new Domain.ValueObjects.DescricaoVO("Wesley Santana");
        var Descricao02 = new Domain.ValueObjects.DescricaoVO("Wesley Santana");

        Assert.True(Descricao01 == Descricao02);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var Descricao01 = new Domain.ValueObjects.DescricaoVO("Wesley Santana");
        var Descricao02 = new Domain.ValueObjects.DescricaoVO("Rita Santana");

        Assert.True(Descricao01 != Descricao02);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var Descricao01 = new Domain.ValueObjects.DescricaoVO("Wesley Santana");
        var Descricao02 = new Domain.ValueObjects.DescricaoVO("Wesley Santana");

        Assert.True(Descricao01.GetHashCode().Equals(Descricao02.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var Descricao01 = new Domain.ValueObjects.DescricaoVO("Wesley Santana");
        var Descricao02 = new Domain.ValueObjects.DescricaoVO("Rita Santana");

        Assert.False(Descricao01.GetHashCode().Equals(Descricao02.GetHashCode()));
    }
}