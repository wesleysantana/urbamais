using Core.Domain;
using Urbamais.Domain.Entities.Fornecedor;
using Urbamais.Domain.Entities.Obra;

namespace Urbamais.Domain.Entities.CoreRelationManyToMany;

public sealed class Endereco : EnderecoCore
{
    public ICollection<Colaborador>? Colaboradores { get; private set; }
    public ICollection<Empresa>? Empresas { get; private set; }
    public ICollection<Fornecedor.Fornecedor>? Fornecedores { get; private set; }
    public new int CidadeId { get; private set; }
    public new Cidade? Cidade { get; private set; }

    public Endereco(string logradouro, string numero, string complemento, string bairro, int cidadeId)
        : base(logradouro, numero, complemento, bairro, cidadeId)
    {
    }
}