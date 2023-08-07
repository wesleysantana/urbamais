using Core.ValueObjects;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Resources;

namespace Urbamais.Domain.Entities.Fornecedores;

public class Fornecedor : PessoaJuridica
{
    public ICollection<Equipamento>? Equipamentos { get; private set; }

    protected Fornecedor()
    { }

    public Fornecedor(string idUserCreation, Nome nomeFantasia, Nome razaoSocial, Cnpj cnpj, string ie,
        string? im, List<Endereco> listaEndereco, List<Telefone>? listaTelefone, List<Email>? listaEmail)
        : base(idUserCreation, nomeFantasia, razaoSocial, cnpj, ie, im, listaEndereco, listaTelefone, listaEmail) { }
    
}