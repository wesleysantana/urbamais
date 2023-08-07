using Core.Domain;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Domain.Entities.Suprimentos;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Endereco : EnderecoCore
{
    public ICollection<Colaborador>? Colaboradores { get; private set; }
    public ICollection<Empresa>? Empresas { get; private set; }
    public ICollection<Fornecedor>? Fornecedores { get; private set; }
    public ICollection<Compra>? Compras { get; private set; }
    public new int CidadeId { get; private set; }
    public new Cidade? Cidade { get; private set; }

    public Endereco(string idUserCreation, string logradouro, string numero, string complemento, string bairro, 
        string codigoPostal, int cidadeId)
        : base(idUserCreation, logradouro, numero, complemento, bairro, codigoPostal, cidadeId)
    {
    }
}