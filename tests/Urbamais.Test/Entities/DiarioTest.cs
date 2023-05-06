using Core.ValueObjects;
using Urbamais.Domain.Entities.Obra;

namespace Urbamais.Test.Entities;

public class DiarioTest
{
    [Fact]
    public void CadastroCorreto()
    {
        var obra = new Obra(EmpresaTest.CadastroEmpresa(), new DescricaoVO("Obra Teste"));
        var fornecedor = FornecedorTest.CadastroFornecedor();
        //var diario = new Diario(obra, fornecedor, "Descrição das atividade", )
    }
}