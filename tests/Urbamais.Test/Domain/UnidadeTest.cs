using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.Test.Domain;

public class UnidadeTest
{
    [Fact]
    public void Ctor_ComDadosValidos_DeveCriarNormalizandoSigla()
    {
        var u = new Unidade("  Engenharia Civil  ", "ec");
        Assert.Equal("Engenharia Civil", u.Descricao);
        Assert.Equal("ec", u.Sigla);
        Assert.Null(u.DataAlteracao);
        Assert.Null(u.DataExclusao);
    }

    [Fact]
    public void Update_ApenasDescricao_DeveAtualizarDescricaoEDataAlteracao()
    {
        var u = new Unidade("Obras", "OB");
        var antes = u.DataAlteracao;

        u.Update("  Obras Urbanas  ", null);

        Assert.Equal("Obras Urbanas", u.Descricao);
        Assert.Equal("OB", u.Sigla); // inalterada
        Assert.NotEqual(antes, u.DataAlteracao);
    }

    [Fact]
    public void Update_ApenasSigla_DeveAtualizarSiglaNormalizadaEDataAlteracao()
    {
        var u = new Unidade("Obras", "OB");
        var antes = u.DataAlteracao;

        u.Update(null, "  ru ");

        Assert.Equal("Obras", u.Descricao);
       
        Assert.Equal("ru", u.Sigla);
        Assert.NotEqual(antes, u.DataAlteracao);
    }

    [Fact]
    public void DeleteERestore_DeveMarcarEReverterExclusaoLogica()
    {
        var u = new Unidade("Obras", "OB");
        Assert.Null(u.DataExclusao);

        u.Delete();
        Assert.NotNull(u.DataExclusao);

        u.Restore();
        Assert.Null(u.DataExclusao);
    }

    [Fact]
    public void Update_SemMudanca_NaoDeveQuebrar()
    {
        var u = new Unidade("Obras", "OB");
        u.Update("Obras", "OB");
        Assert.Equal("Obras", u.Descricao);
        Assert.Equal("OB", u.Sigla);
    }
}