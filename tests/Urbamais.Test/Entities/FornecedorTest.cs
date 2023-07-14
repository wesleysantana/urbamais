using Core.ValueObjects;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Supplier;

namespace Urbamais.Test.Entities;

public class FornecedorTest
{
    public static Supplier CadastroFornecedor()
    {
        var nome = new NameVO("Fornecedor Teste");
        var razao = new NameVO("Fornecedor Teste LTDA");
        var cnpj = new CnpjVO("11.587.881/0001-05");

        var endereco = new List<Address>()
        {
            new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

        List<Phone> telefones = new()
        {
            new Phone("112313213"),
            new Phone("221561511")
        };

        List<Email> emails = new()
        {
            new Email("wel.santana@hotmail.com"),
            new Email("rita.santana@hotmail.com")
        };

        return new Supplier(nome, razao, cnpj, "123.456-45", null, endereco, telefones, emails);
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

        var endereco = new List<Address>()
        {
            new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

        Fornecedor.Update(new NameVO("Novo Nome Razão"), new NameVO("novo Nome"), new CnpjVO("11.587.881/0001-05"), endereco);
        Assert.True(Fornecedor.IsValid);
    }

    [Fact]
    public void UpdateInvalido()
    {
        var Fornecedor = CadastroFornecedor();
        Assert.True(Fornecedor.IsValid);

        var endereco = new List<Address>()
        {
            new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

        Fornecedor.Update(new NameVO(""), new NameVO("novo Nome"), new CnpjVO("11.587.881/0001-05"), endereco);
        Assert.False(Fornecedor.IsValid);
    }

    [Fact]
    public void NomeIncorreto()
    {
        var nome = new NameVO("");
        var razao = new NameVO("Fornecedor Teste LTDA");
        var cnpj = new CnpjVO("11.587.881/0001-05");

        var endereco = new List<Address>()
        {
            new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

        List<Phone> telefones = new()
        {
            new Phone("112313213"),
            new Phone("221561511")
        };

        List<Email> emails = new()
        {
            new Email("wel.santana@hotmail.com"),
            new Email("rita.santana@hotmail.com")
        };

        var Fornecedor = new Supplier(razao, nome, cnpj, "123.456-45", null, endereco, telefones, emails);
        Assert.False(Fornecedor.IsValid);
    }

    [Fact]
    public void TelefoneIncorreto()
    {
        var nome = new NameVO("Fornecedor Teste");
        var razao = new NameVO("Fornecedor Teste LTDA");
        var cnpj = new CnpjVO("11.587.881/0001-05");

        var endereco = new List<Address>()
        {
            new Address("Rua Tito Lívio Brasil", "70", "", "Vila Verinha", "19040170", 1)
        };

        var numeroLong = "112313213000000000001111111";
        List<Phone> telefones = new()
        {
            new Phone(numeroLong),
            new Phone("221561511"),
            new Phone(""),
        };

        List<Email> emails = new()
        {
            new Email("wel.santana@hotmail.com"),
            new Email("rita.santana@hotmail.com")
        };

        var Fornecedor = new Supplier(razao, nome, cnpj, "123.456-45", null, endereco, telefones, emails);

        var msg = $"'Numero' must not be empty.";
        Assert.Contains(Fornecedor.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));

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
        var newList = Fornecedor2.Phones.ToList();
        newList.Add(new Phone("123456789"));
        Fornecedor2.Update(phones: newList);

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
        Fornecedor2.Update(tradeName: new NameVO("Novo Nome"));

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
        Fornecedor2.Update(corporateName: new NameVO("Nova Razão Social"));

        Assert.False(Fornecedor1.GetHashCode().Equals(Fornecedor2.GetHashCode()));
    }
}