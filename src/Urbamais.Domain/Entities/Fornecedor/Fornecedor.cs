using Core.ValueObjects;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Resources;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Fornecedor : PessoaJuridica
{
    public ICollection<Equipamento>? Equipamentos { get; private set; }

    protected Fornecedor()
    { }

    public Fornecedor(NomeVO nomeFantasia, NomeVO razaoSocial, CnpjVO cnpj, string inscricaoEstadual,
        string? inscricaoMunicipal, List<Endereco> listEndereco, List<Telefone>? listTelefone, List<Email>? listEmail)
        : base(nomeFantasia, razaoSocial, cnpj, inscricaoEstadual, inscricaoMunicipal, listEndereco, listTelefone, listEmail)
    { }
}