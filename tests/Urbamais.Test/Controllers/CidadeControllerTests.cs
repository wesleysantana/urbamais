using Microsoft.AspNetCore.Mvc;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Test.Fakes;
using Urbamais.WebApi.Controllers.v1;

namespace Urbamais.Test.Controllers;
public class CidadeControllerTests
{
    [Fact]
    public async Task BuscarPorUf_ComUfInvalida_DeveRetornar400()
    {
        ICidadeAppService app = new FakeCidadeAppService();
        var ctrl = new CidadeController(app);
        
        var result = await ctrl.BuscarPorUf("ZZ", CancellationToken.None);
        var problem = Assert.IsType<ObjectResult>(result);
        Assert.Equal(400, problem.StatusCode);
    }

    [Fact]
    public async Task Buscar_SemUf_DeveRetornar200()
    {
        ICidadeAppService app = new FakeCidadeAppService();
        var ctrl = new CidadeController(app);

        var action = await ctrl.Buscar("Rio", "", 50, CancellationToken.None);
        Assert.IsType<OkObjectResult>(action);
    }
}
