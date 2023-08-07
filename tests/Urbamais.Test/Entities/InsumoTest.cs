using Core.ValueObjects;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Test.Entities;

public class InsumoTest
{
    //[Fact]
    //public void CadastroDeInsumoCorreto()
    //{
    //    var insumo = new Insumo(new NomeVO("Insumo"), "Descrição qualquer", new Unidade("MT"), TipoInsumo.Material);
    //    Assert.True(insumo.IsValid);

    //    insumo = new Insumo(new NomeVO("Insumo"), "Descrição qualquer", new Unidade("MT"), TipoInsumo.Insumo);
    //    Assert.True(insumo.IsValid);
    //}

    //[Fact]
    //public void TipoIncorreto()
    //{
    //    var tipoInsumo = TipoInsumo.Material - 1;
    //    var insumo = new Insumo(new NomeVO("Insumo"), "Descrição qualquer", new Unidade("MT"), tipoInsumo);
    //    Assert.False(insumo.IsValid);

    //    var msg = $"'{nameof(insumo.Tipo)}' has a range of values which does not include '{tipoInsumo}'.";
    //    Assert.Contains(insumo.ValidationResult.Errors, x => x.ErrorMessage.Equals(msg));
    //}
}