using Core.ValueObjects;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Resources;

namespace Urbamais.Domain.Entities.Obras;

public class Empresa : PessoaJuridica
{
    public ICollection<Obra>? Constructions { get; private set; }

    protected Empresa() { }

    public Empresa(string idUserCreation, Nome nomeFantasia, Nome razaoSocial, Cnpj cnpj, string ie,
    string? im, List<Endereco> listaEndereco, List<Telefone>? listaTelefone, List<Email>? listaEmail)
    : base(idUserCreation, nomeFantasia, razaoSocial, cnpj, ie, im, listaEndereco, listaTelefone, listaEmail) { }
}
