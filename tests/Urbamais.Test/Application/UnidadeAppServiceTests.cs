using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urbamais.Application.Services.Planejamento;
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Test.Fakes;

namespace Urbamais.Test.Application;
public class UnidadeAppServiceTests
{
    private readonly FakeUnitOfWork _uow = new();
    private readonly FakeUnidadeRepository _repo = new();

    private UnidadeAppService CreateSut() => new UnidadeAppService(_repo, _uow);

    [Fact]
    public async Task CadastrarAsync_QuandoSiglaDuplicada_DeveRetornarConflict()
    {
        var sut = CreateSut();
        await sut.CadastrarAsync(new UnidadeRequest { Descricao = "A", Sigla = "AA" }, CancellationToken.None);
        var r = await sut.CadastrarAsync(new UnidadeRequest { Descricao = "Outra", Sigla = "aa" }, CancellationToken.None);

        Assert.False(r.IsSuccess);
        Assert.Equal("conflict", r.Code);
    }

    [Fact]
    public async Task CadastrarAsync_Sucesso_DeveRetornarOkComId()
    {
        var sut = CreateSut();
        var r = await sut.CadastrarAsync(new UnidadeRequest { Descricao = "Obras", Sigla = "OB" }, CancellationToken.None);

        Assert.True(r.IsSuccess);
        Assert.True(r.Value > 0);
        Assert.Equal(1, _uow.SaveCount);
    }

    [Fact]
    public async Task AtualizarAsync_SemAlterarSigla_NaoChecaUnicidade()
    {
        var sut = CreateSut();
        var create = await sut.CadastrarAsync(new UnidadeRequest { Descricao = "Obras", Sigla = "OB" }, CancellationToken.None);
        var r = await sut.AtualizarAsync(create.Value, new UnidadeUpdateRequest { Descricao = "Obras Urbanas" }, CancellationToken.None);

        Assert.True(r.IsSuccess);
        Assert.Equal(2, _uow.SaveCount);
    }

    [Fact]
    public async Task AtualizarAsync_TrocandoParaSiglaExistente_DeveRetornarConflict()
    {
        var sut = CreateSut();
        var a = await sut.CadastrarAsync(new UnidadeRequest { Descricao = "A", Sigla = "AA" }, CancellationToken.None);
        var b = await sut.CadastrarAsync(new UnidadeRequest { Descricao = "B", Sigla = "BB" }, CancellationToken.None);

        var r = await sut.AtualizarAsync(b.Value, new UnidadeUpdateRequest { Sigla = "aa" }, CancellationToken.None);

        Assert.False(r.IsSuccess);
        Assert.Equal("conflict", r.Code);
    }

    [Fact]
    public async Task ExcluirAsync_Soft_DeveMarcarDataExclusaoEChamarUoW()
    {
        var sut = CreateSut();
        var created = await sut.CadastrarAsync(new UnidadeRequest { Descricao = "Obras", Sigla = "OB" }, CancellationToken.None);

        var r = await sut.ExcluirAsync(created.Value, CancellationToken.None);
        var u = await _repo.GetByIdAsync(created.Value, CancellationToken.None);

        Assert.True(r.IsSuccess);
        Assert.NotNull(u!.DataExclusao);
        Assert.True(_uow.SaveCount >= 2);
    }

    [Fact]
    public async Task ExcluirDefinitivoAsync_Hard_DeveRemoverDaColecao()
    {
        var sut = CreateSut();
        var created = await sut.CadastrarAsync(new UnidadeRequest { Descricao = "Obras", Sigla = "OB" }, CancellationToken.None);

        var r = await sut.ExcluirDefinitivoAsync(created.Value, CancellationToken.None);
        var u = await _repo.GetByIdAsync(created.Value, CancellationToken.None);

        Assert.True(r.IsSuccess);
        Assert.Null(u);
    }
}
