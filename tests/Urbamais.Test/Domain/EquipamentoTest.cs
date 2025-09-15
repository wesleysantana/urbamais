using Core.ValueObjects;
using Urbamais.Domain.Entities.Fornecedor;

namespace Urbamais.Test.Domain;

public class EquipamentoTest
{
    [Fact]
    public void CadastroCorreto()
    {
        var equipamento = new Equipamento(new NomeVO("Novo Equipamento"), new DescricaoVO("Descrição Qualquer"));
        Assert.True(equipamento.IsValid);
    }

    [Fact]
    public void CadastroComNomeIncorreto()
    {
        var equipamento = new Equipamento(new NomeVO(""), new DescricaoVO("Descrição Qualquer"));
        Assert.False(equipamento.IsValid);

        var msg = $"The length of '{nameof(equipamento.Nome)}' must be at least 3 characters. You entered 0 characters.";
        Assert.Contains(equipamento.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void CadastroComDescricaoIncorreta()
    {
        var valor = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
        Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an
        unknown printer took a galley of type and scrambled it to make a type specimen book.
        It has survived not only five centuries, but also the leap into electronic typesetting,
        remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset
        sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like
        Aldus PageMaker including versions of Lorem Ipsum.";

        var descricao = new DescricaoVO(valor);
        Assert.False(descricao.IsValid);

        var msg = $"The length of '{nameof(descricao.Descricao)}' must be 255 characters or fewer. You entered {valor.Length} characters.";
        Assert.Contains(descricao.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Update()
    {
        var descricao = new DescricaoVO("Equipamento Alterada");
        var equipamento = new Equipamento(new NomeVO("Novo Equipamento"), new DescricaoVO("Descrição Qualquer"));
        Assert.True(equipamento.IsValid);

        equipamento.Update(descricao: descricao);
        Assert.Equal(descricao, equipamento.Descricao);
        Assert.True(equipamento.IsValid);
    }   
}