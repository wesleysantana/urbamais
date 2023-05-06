using Core.Domain;

namespace Urbamais.Test.Entities;

public class UfTest
{
    [Fact]
    public void UfValida()
    {
        var uf = new Uf("pe");

        Assert.True(uf.IsValid);
    }

    [Fact]
    public void UfInvalida()
    {
        var uf = new Uf("oo");

        Assert.False(uf.IsValid);

        var msg = $"The specified condition was not met for '{nameof(uf.Sigla)}'.";
        Assert.Contains(uf.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void UfEmBrano()
    {
        var uf = new Uf("");

        Assert.False(uf.IsValid);

        var msg = $"'{nameof(uf.Sigla)}' must be 2 characters in length. You entered 0 characters.";
        Assert.Contains(uf.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var uf1 = new Uf("pe");
        var uf2 = new Uf("pe");

        Assert.True(uf1.Equals(uf2));
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var uf1 = new Uf("pe");
        var uf2 = new Uf("sp");

        Assert.False(uf1.Equals(uf2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var uf1 = new Uf("pe");
        var uf2 = new Uf("pe");

        Assert.True(uf1 == uf2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var uf1 = new Uf("pe");
        var uf2 = new Uf("sp");

        Assert.True(uf1 != uf2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var uf1 = new Uf("pe");
        var uf2 = new Uf("pe");

        Assert.True(uf1.GetHashCode().Equals(uf2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var uf1 = new Uf("pe");
        var uf2 = new Uf("sp");

        Assert.False(uf1.GetHashCode().Equals(uf2.GetHashCode()));
    }
}