using Core.Domain;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.Entities.Supplier;
using Urbamais.Domain.Entities.Supply;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Endereco : EnderecoCore
{
    public ICollection<Collaborator>? Colaboradores { get; private set; }
    public ICollection<Empresa>? Empresas { get; private set; }
    public ICollection<Supplier.Supplier>? Fornecedores { get; private set; }
    public ICollection<Purchase>? Compras { get; private set; }
    public new int CidadeId { get; private set; }
    public new Cidade? Cidade { get; private set; }

    public Endereco(string idUserCreation, string logradouro, string numero, string complemento, string bairro, 
        string codigoPostal, int cidadeId)
        : base(idUserCreation, logradouro, numero, complemento, bairro, codigoPostal, cidadeId)
    {
    }
}