using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Application.ViewModels.Response.v1.Unidade;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Test.Fakes;
using Urbamais.WebApi.Controllers.v1;

namespace Urbamais.Test.Controllers;
public class UnidadeControllerTests
{
    private sealed class PassthroughProfile : Profile
    {
        public PassthroughProfile()
        {
            CreateMap<Unidade, UnidadeResponse>();
        }
    }

    private (UnidadeController ctrl, FakeUnidadeAppService app) CreateSut()
    {
        var app = new FakeUnidadeAppService();
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile<PassthroughProfile>()).CreateMapper();
        var ctrl = new UnidadeController(app, mapper);
        return (ctrl, app);
    }

    [Fact]
    public async Task GetById_QuandoNaoExiste_DeveRetornarNotFound()
    {
        var (ctrl, _) = CreateSut();
        var result = await ctrl.GetById(123, CancellationToken.None);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Post_CriaERetorna201()
    {
        var (ctrl, _) = CreateSut();
        var req = new UnidadeRequest { Descricao = "Obras", Sigla = "OB" };
        var result = await ctrl.Post(req, CancellationToken.None);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(StatusCodes.Status201Created, created.StatusCode);
    }
}
