using Core.ValueObjects;
using Urbamais.Domain.Entities.Obra;

namespace Urbamais.Test.Entities;

public class ObraTest
{
    [Fact]
    public void CadastroCorreto()
    {
        var obra = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        Assert.True(obra.IsValid);
    }

    [Fact]
    public void Update()
    {
        var descricao = new DescricaoVO("Obra Alterada");
        var obra = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));

        obra.Update(descricao);
        Assert.Equal(descricao, obra.Descricao);
        Assert.True(obra.IsValid);
    }

    [Fact]
    public void Igualdade()
    {
        var obra1 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        var obra2 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));

        Assert.True(obra1.Equals(obra2));
    }

    [Fact]
    public void UpdateEIgualdadeFalha()
    {
        var obra1 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        var obra2 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));

        obra2.Update(new DescricaoVO("Obra Alterada"));

        Assert.False(obra1.Equals(obra2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var empresa1 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        var empresa2 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));

        Assert.True(empresa1 == empresa2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var obra1 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        var obra2 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        obra2.Update(new DescricaoVO("Obra Alterada"));

        Assert.True(obra1 != obra2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var obra1 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        var obra2 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));

        Assert.True(obra1.GetHashCode().Equals(obra2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var obra1 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        var obra2 = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        obra2.Update(new DescricaoVO("Obra Alterada"));

        Assert.False(obra1.GetHashCode().Equals(obra2.GetHashCode()));
    }
}