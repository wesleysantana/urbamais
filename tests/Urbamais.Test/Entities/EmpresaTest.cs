using Urbamais.Domain.Entities.Core;
using Urbamais.Domain.Entities.Obra;
using Urbamais.Domain.ValueObjects;

namespace Urbamais.Test.Entities;

public class EmpresaTest
{
    public static Empresa CadastroEmpresa()
    {
        var nome = new NomeVO("Empresa Teste");
        var razao = new NomeVO("Empresa Teste LTDA");
        var cnpj = new CnpjVO("11.587.881/0001-05");

        var endereco = new Endereco("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", 
            new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP")));

        List<Telefone> telefones = new()
        {
            new Telefone("112313213"),
            new Telefone("221561511")
        };

        List<Email> emails = new()
        {
            new Email("wel.santana@hotmail.com"),
            new Email("rita.santana@hotmail.com")
        };

        return new Empresa(razao, nome, cnpj, endereco, telefones, emails);
    }

    [Fact]
    public void CadastroEmpresaCorreto()
    {
        var empresa = CadastroEmpresa();
        Assert.True(empresa.IsValid);
    }

    [Fact]
    public void UpdateValido()
    {
        var empresa = CadastroEmpresa();
        Assert.True(empresa.IsValid);
        
        empresa.Update(new NomeVO("Novo Nome Razão"), new NomeVO("novo Nome"), new CnpjVO("11.587.881/0001-05"),
            new Endereco("Rua Tal", "100", "", "Bairro Qualquer", new Cidade(new NomeVO("Pirapozinho"), new Uf("SP"))));
        Assert.True(empresa.IsValid);
    }

    [Fact]
    public void UpdateInvalido()
    {
        var empresa = CadastroEmpresa();
        Assert.True(empresa.IsValid);

        empresa.Update(new NomeVO(""), new NomeVO("novo Nome"), new CnpjVO("11.587.881/0001-05"),
            new Endereco("Rua Tal", "100", "", "Bairro Qualquer", new Cidade(new NomeVO("Pirapozinho"), new Uf("SP"))));
        Assert.False(empresa.IsValid);
    }

    [Fact]
    public void NomeIncorreto()
    {
        var nome = new NomeVO("");
        var razao = new NomeVO("Empresa Teste LTDA");
        var cnpj = new CnpjVO("11.587.881/0001-05");

        var endereco = new Endereco("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", 
            new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP")));

        List<Telefone> telefones = new()
        {
            new Telefone("112313213"),
            new Telefone("221561511")
        };

        List<Email> emails = new()
        {
            new Email("wel.santana@hotmail.com"),
            new Email("rita.santana@hotmail.com")
        };

        var empresa = new Empresa(razao, nome, cnpj, endereco, telefones, emails);
        Assert.False(empresa.IsValid);
    }

    [Fact]
    public void TelefoneIncorreto()
    {
        var nome = new NomeVO("Empresa Teste");
        var razao = new NomeVO("Empresa Teste LTDA");
        var cnpj = new CnpjVO("11.587.881/0001-05");

        var endereco = new Endereco("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", 
            new Cidade(new NomeVO("Presidente Prudente"), new Uf("SP")));

        var numeroLong = "112313213000000000001111111";
        List<Telefone> telefones = new()
        {
            new Telefone(numeroLong),
            new Telefone("221561511"),
            new Telefone(""),
        };

        List<Email> emails = new()
        {
            new Email("wel.santana@hotmail.com"),
            new Email("rita.santana@hotmail.com")
        };

        var empresa = new Empresa(razao, nome, cnpj, endereco, telefones, emails);

        var msg = $"'Numero' must not be empty.";
        Assert.Contains(empresa.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));

        msg = $"The length of 'Numero' must be 20 characters or fewer. " +
           $"You entered {numeroLong.Length} characters.";
        Assert.Contains(empresa.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var empresa1 = CadastroEmpresa();
        var empresa2 = CadastroEmpresa();

        Assert.True(empresa1.Equals(empresa2));
    }

    [Fact]
    public void UpdateEIgualdadeFalha()
    {
        var empresa1 = CadastroEmpresa();
        var empresa2 = CadastroEmpresa();
        var newList = empresa2.Telefones.ToList();
        newList.Add(new Telefone("123456789"));
        empresa2.Update(telefones: newList);

        Assert.False(empresa1.Equals(empresa2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var empresa1 = CadastroEmpresa();
        var empresa2 = CadastroEmpresa();

        Assert.True(empresa1 == empresa2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var empresa1 = CadastroEmpresa();
        var empresa2 = CadastroEmpresa();        
        empresa2.Update(nomeFantasia: new NomeVO("Novo Nome"));

        Assert.True(empresa1 != empresa2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var empresa1 = CadastroEmpresa();
        var empresa2 = CadastroEmpresa();

        Assert.True(empresa1.GetHashCode().Equals(empresa2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var empresa1 = CadastroEmpresa();
        var empresa2 = CadastroEmpresa();
        empresa2.Update(razaoSocial: new NomeVO("Nova Razão Social"));

        Assert.False(empresa1.GetHashCode().Equals(empresa2.GetHashCode()));
    }
}