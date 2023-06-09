using Core.ValueObjects;
using Urbamais.Domain.Entities.CoreRelationManyToMany;
using Urbamais.Domain.Entities.Fornecedor;

namespace Urbamais.Test.Entities;

public class FornecedorTest
{
    public static Fornecedor CadastroFornecedor()
    {
        var nome = new NomeVO("Fornecedor Teste");
        var razao = new NomeVO("Fornecedor Teste LTDA");
        var cnpj = new CnpjVO("11.587.881/0001-05");

        var endereco = new List<Endereco>()
        {
            new Endereco("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

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

        return new Fornecedor(nome, razao, cnpj, "123.456-45", null, endereco, telefones, emails);
    }

    [Fact]
    public void CadastroFornecedorCorreto()
    {
        var Fornecedor = CadastroFornecedor();
        Assert.True(Fornecedor.IsValid);
    }

    [Fact]
    public void UpdateValido()
    {
        var Fornecedor = CadastroFornecedor();
        Assert.True(Fornecedor.IsValid);

        var endereco = new List<Endereco>()
        {
            new Endereco("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

        Fornecedor.Update(new NomeVO("Novo Nome Razão"), new NomeVO("novo Nome"), new CnpjVO("11.587.881/0001-05"), endereco);
        Assert.True(Fornecedor.IsValid);
    }

    [Fact]
    public void UpdateInvalido()
    {
        var Fornecedor = CadastroFornecedor();
        Assert.True(Fornecedor.IsValid);

        var endereco = new List<Endereco>()
        {
            new Endereco("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

        Fornecedor.Update(new NomeVO(""), new NomeVO("novo Nome"), new CnpjVO("11.587.881/0001-05"), endereco);
        Assert.False(Fornecedor.IsValid);
    }

    [Fact]
    public void NomeIncorreto()
    {
        var nome = new NomeVO("");
        var razao = new NomeVO("Fornecedor Teste LTDA");
        var cnpj = new CnpjVO("11.587.881/0001-05");

        var endereco = new List<Endereco>()
        {
            new Endereco("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

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

        var Fornecedor = new Fornecedor(razao, nome, cnpj, "123.456-45", null, endereco, telefones, emails);
        Assert.False(Fornecedor.IsValid);
    }

    [Fact]
    public void TelefoneIncorreto()
    {
        var nome = new NomeVO("Fornecedor Teste");
        var razao = new NomeVO("Fornecedor Teste LTDA");
        var cnpj = new CnpjVO("11.587.881/0001-05");

        var endereco = new List<Endereco>()
        {
            new Endereco("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

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

        var Fornecedor = new Fornecedor(razao, nome, cnpj, "123.456-45", null, endereco, telefones, emails);

        var msg = $"'Numero' must not be empty.";
        Assert.Contains(Fornecedor.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));

        msg = $"The length of 'Numero' must be 20 characters or fewer. " +
           $"You entered {numeroLong.Length} characters.";
        Assert.Contains(Fornecedor.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Igualdade()
    {
        var Fornecedor1 = CadastroFornecedor();
        var Fornecedor2 = CadastroFornecedor();

        Assert.True(Fornecedor1.Equals(Fornecedor2));
    }

    [Fact]
    public void UpdateEIgualdadeFalha()
    {
        var Fornecedor1 = CadastroFornecedor();
        var Fornecedor2 = CadastroFornecedor();
        var newList = Fornecedor2.Telefones.ToList();
        newList.Add(new Telefone("123456789"));
        Fornecedor2.Update(telefones: newList);

        Assert.False(Fornecedor1.Equals(Fornecedor2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var Fornecedor1 = CadastroFornecedor();
        var Fornecedor2 = CadastroFornecedor();

        Assert.True(Fornecedor1 == Fornecedor2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var Fornecedor1 = CadastroFornecedor();
        var Fornecedor2 = CadastroFornecedor();
        Fornecedor2.Update(nomeFantasia: new NomeVO("Novo Nome"));

        Assert.True(Fornecedor1 != Fornecedor2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var Fornecedor1 = CadastroFornecedor();
        var Fornecedor2 = CadastroFornecedor();

        Assert.True(Fornecedor1.GetHashCode().Equals(Fornecedor2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var Fornecedor1 = CadastroFornecedor();
        var Fornecedor2 = CadastroFornecedor();
        Fornecedor2.Update(razaoSocial: new NomeVO("Nova Razão Social"));

        Assert.False(Fornecedor1.GetHashCode().Equals(Fornecedor2.GetHashCode()));
    }
}