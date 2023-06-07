using Core.ValueObjects;
using Urbamais.Domain.Entities.CoreRelationManyToMany;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Fornecedor : PessoaJuridica
{
    protected Fornecedor()
    { }

    public Fornecedor(NomeVO nomeFantasia, NomeVO razaoSocial, CnpjVO cnpj, string inscricaoEstadual,
        string? inscricaoMunicipal, List<Endereco> listEndereco, List<Telefone>? listTelefone, List<Email>? listEmail)
        : base(nomeFantasia, razaoSocial, cnpj, inscricaoEstadual, inscricaoMunicipal, listEndereco, listTelefone, listEmail)
    { }
}