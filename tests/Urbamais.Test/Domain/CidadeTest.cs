using Core.Domain;
using Core.ValueObjects;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Test.Domain;

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
        Assert.Contains(cidade.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    } 
}