using Core.Domain;
using Core.ValueObjects;

namespace Urbamais.Test.Entities;

public class CidadeTest
{
    [Fact]
    public void CidadeCorreta()
    {
        var cidade = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);

        Assert.True(cidade.IsValid);
    }

    [Fact]
    public void UpdateCorreto()
    {
        var cidade = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);
        Assert.True(cidade.IsValid);

        cidade.Update(uf: Uf.PE);
        Assert.True(cidade.IsValid);
    }

    [Fact]
    public void UpdateIncorreto()
    {
        var cidade = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);
        Assert.True(cidade.IsValid);

        cidade.Update(new NomeVO(""));
        Assert.False(cidade.IsValid);
    }

    [Fact]
    public void NomeCidadeVazia()
    {
        var cidade = new Cidade(new NomeVO(""), Uf.SP);

        Assert.False(cidade.IsValid);

        var msg = $"The length of '{nameof(cidade.Nome)}' must be at least 3 characters. You entered 0 characters.";
        Assert.Contains(cidade.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }   

    [Fact]
    public void Igualdade()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);

        Assert.True(cidade1.Equals(cidade2));
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), Uf.PE);

        Assert.False(cidade1.Equals(cidade2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);

        Assert.True(cidade1 == cidade2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), Uf.PE);

        Assert.True(cidade1 != cidade2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);

        Assert.True(cidade1.GetHashCode().Equals(cidade2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), Uf.SP);
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), Uf.PE);

        Assert.False(cidade1.GetHashCode().Equals(cidade2.GetHashCode()));
    }
}