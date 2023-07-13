using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Test.Entities;

public class UnidadeTest
{
    //[Fact]
    //public void UnidadeValida()
    //{
    //    var unidade = new Unidade("Metro");
    //    Assert.True(unidade.IsValid);
    //}

    //[Fact]
    //public void UnidadeComDescricaoVazia()
    //{
    //    var unidade = new Unidade("");

    //    Assert.False(unidade.IsValid);
    //    var msg = $"'{nameof(unidade.Descricao)}' must not be empty.";
    //    Assert.Contains(unidade.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    //}

    //[Fact]
    //public void UnidadeComQuantidadeDeCaracteresMaiorQue50()
    //{
    //    var descricao = "MetroMetroMetroMetroMetroMetroMetroMetroMetroMetroMetro";
    //    var unidade = new Unidade(descricao);

    //    Assert.False(unidade.IsValid);
    //    var msg = $"The length of '{nameof(unidade.Descricao)}' must be 50 characters or fewer. You entered {descricao.Length} characters.";
    //    Assert.Contains(unidade.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    //}

    //[Fact]
    //public void Igualdade()
    //{
    //    var unidade1 = new Unidade("Metro");
    //    var unidade2 = new Unidade("Metro");

    //    Assert.True(unidade1.Equals(unidade2));
    //}

    //[Fact]
    //public void IgualdadeFalha()
    //{
    //    var unidade1 = new Unidade("Metro");
    //    var unidade2 = new Unidade("Litro");

    //    Assert.False(unidade1.Equals(unidade2));
    //}

    //[Fact]
    //public void IgualdadeOperator()
    //{
    //    var unidade1 = new Unidade("Metro");
    //    var unidade2 = new Unidade("Metro");

    //    Assert.True(unidade1 == unidade2);
    //}

    //[Fact]
    //public void IgualdadeFalhaOperator()
    //{
    //    var unidade1 = new Unidade("Metro");
    //    var unidade2 = new Unidade("Litro");

    //    Assert.True(unidade1 != unidade2);
    //}

    //[Fact]
    //public void IgualdadeHashcode()
    //{
    //    var unidade1 = new Unidade("Metro");
    //    var unidade2 = new Unidade("Metro");

    //    Assert.True(unidade1.GetHashCode().Equals(unidade2.GetHashCode()));
    //}

    //[Fact]
    //public void IgualdadeHashcodeFalha()
    //{
    //    var unidade1 = new Unidade("Metro");
    //    var unidade2 = new Unidade("Litro");

    //    Assert.False(unidade1.GetHashCode().Equals(unidade2.GetHashCode()));
    //}
}