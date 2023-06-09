using Core.ValueObjects;
using Urbamais.Domain.Entities.CoreRelationManyToMany;
using Urbamais.Domain.Resources;

namespace Urbamais.Domain.Entities.Obra;

public class Empresa : PessoaJuridica
{
    public ICollection<Obra>? Obras { get; private set; }

    protected Empresa() { }

    public Empresa(NomeVO nomeFantasia, NomeVO razaoSocial, CnpjVO cnpj, string inscricaoEstadual,
        string? inscricaoMunicipal, List<Endereco> listEndereco, List<Telefone>? listTelefone, List<Email>? listEmail)
        : base(nomeFantasia, razaoSocial, cnpj, inscricaoEstadual, inscricaoMunicipal, listEndereco, listTelefone, listEmail) { }
}