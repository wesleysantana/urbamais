using Core.ValueObjects;
using Urbamais.Domain.Entities.Supplier;

namespace Urbamais.Test.Entities;

public class EquipamentoTest
{
    [Fact]
    public void CadastroCorreto()
    {
        var equipamento = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        Assert.True(equipamento.IsValid);
    }

    [Fact]
    public void CadastroComNomeIncorreto()
    {
        var equipamento = new Equipment(new NameVO(""), new DescriptionVO("Descrição Qualquer"));
        Assert.False(equipamento.IsValid);

        var msg = $"The length of '{nameof(equipamento.Name)}' must be at least 3 characters. You entered 0 characters.";
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

        var descricao = new DescriptionVO(valor);
        Assert.False(descricao.IsValid);

        var msg = $"The length of '{nameof(descricao.Description)}' must be 255 characters or fewer. You entered {valor.Length} characters.";
        Assert.Contains(descricao.ValidationResult!.Errors, x => x.ErrorMessage.Equals(msg));
    }

    [Fact]
    public void Update()
    {
        var descricao = new DescriptionVO("Equipamento Alterada");
        var equipamento = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        Assert.True(equipamento.IsValid);

        equipamento.Update(description: descricao);
        Assert.Equal(descricao, equipamento.Description);
        Assert.True(equipamento.IsValid);
    }

    [Fact]
    public void Igualdade()
    {
        var equipamento1 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        var equipamento2 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));

        Assert.True(equipamento1.Equals(equipamento2));
    }

    [Fact]
    public void UpdateEIgualdadeFalha()
    {
        var equipamento1 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        var equipamento2 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));

        equipamento2.Update(description: new DescriptionVO("Obra Alterada"));

        Assert.False(equipamento1.Equals(equipamento2));
    }

    [Fact]
    public void IgualdadeOperator()
    {
        var equipamento1 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        var equipamento2 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));

        Assert.True(equipamento1 == equipamento2);
    }

    [Fact]
    public void IgualdadeFalhaOperator()
    {
        var equipamento1 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        var equipamento2 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        equipamento2.Update(description: new DescriptionVO("Obra Alterada"));

        Assert.True(equipamento1 != equipamento2);
    }

    [Fact]
    public void IgualdadeHashcode()
    {
        var equipamento1 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        var equipamento2 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));

        Assert.True(equipamento1.GetHashCode().Equals(equipamento2.GetHashCode()));
    }

    [Fact]
    public void IgualdadeHashcodeFalha()
    {
        var equipamento1 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        var equipamento2 = new Equipment(new NameVO("Novo Equipamento"), new DescriptionVO("Descrição Qualquer"));
        equipamento2.Update(description: new DescriptionVO("Obra Alterada"));

        Assert.False(equipamento1.GetHashCode().Equals(equipamento2.GetHashCode()));
    }
}