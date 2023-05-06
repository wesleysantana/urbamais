using Core.Domain;
using Core.ValueObjects;

namespace Urbamais.Test.Entities;

public class CidadeTest
{
    [Fact]
    public void CidadeCorreta()
    {
        var cidade = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));

        Assert.True(cidade.IsValid);
    }

    [Fact]
    public void UpdateCorreto()
    {
        var cidade = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));
        Assert.True(cidade.IsValid);

        cidade.Update(siglaUf: new Uf("PE"));
        Assert.True(cidade.IsValid);
    }

    [Fact]
    public void UpdateIncorreto()
    {
        var cidade = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));
        Assert.True(cidade.IsValid);

        cidade.Update(siglaUf: new Uf(""));
        Assert.False(cidade.IsValid);
    }

    [Fact]
    public void NomeCidadeVazia()
    {
        var cidade = new Cidade(new NomeVO(""), new Uf("SP"));

        Assert.False(cidade.IsValid);

        var msg = $"The length of '{nameof(cidade.Nome)}' must be at least 3 characters. You entered 0 characters.";
        Assert.Contains(cidade.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void NomeEstadoVazia()
    {
        var cidade = new Cidade(new NomeVO("Presidente Prudente"), new Uf(""));

        Assert.False(cidade.IsValid);

        var msg = $"'{nameof(cidade.Uf.Sigla)}' must be 2 characters in length. You entered 0 characters.";
        Assert.Contains(cidade.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void EstadoIncorreto()
    {
        var cidade = new Cidade(new NomeVO("Presidente Prudente"), new Uf("OO"));

        Assert.False(cidade.IsValid);

        var msg = $"The specified condition was not met for '{nameof(cidade.Uf.Sigla)}'.";
        Assert.Contains(cidade.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));

        Assert.True(cidade1.Equals(cidade2));
    }

    [Fact]
    public void IgualdadeFalha()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("PE"));

        Assert.False(cidade1.Equals(cidade2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));

        Assert.True(cidade1 == cidade2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("PE"));

        Assert.True(cidade1 != cidade2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));

        Assert.True(cidade1.GetHashCode().Equals(cidade2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var cidade1 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP"));
        var cidade2 = new Cidade(new NomeVO("Presidente Prudente"), new Uf("PE"));

        Assert.False(cidade1.GetHashCode().Equals(cidade2.GetHashCode()));
    }
}