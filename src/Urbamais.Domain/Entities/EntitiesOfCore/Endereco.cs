using Core.Domain;
using Urbamais.Domain.Entities.Fornecedor;
using Urbamais.Domain.Entities.Obra;
using Urbamais.Domain.Entities.Suprimento;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Endereco(string logradouro, string numero, string complemento, string bairro, string cep, int cidadeId) 
    : EnderecoCore(logradouro, numero, complemento, bairro, cep, cidadeId)
{
    public ICollection<Colaborador>? Colaboradores { get; private set; }
    public ICollection<Empresa>? Empresas { get; private set; }
    public ICollection<Fornecedor.Fornecedor>? Fornecedores { get; private set; }
    public ICollection<Compra>? Compras { get; private set; }
    public new int CidadeId { get; private set; }
    public new Cidade? Cidade { get; private set; }
}