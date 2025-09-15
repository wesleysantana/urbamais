using Core.Domain;
using Urbamais.Domain.Entities.Fornecedor;
using Urbamais.Domain.Entities.Obra;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Email(string endereco) : EmailCore(endereco)
{
    public ICollection<Colaborador>? Colaboradores { get; private set; }
    public ICollection<Empresa>? Empresas { get; private set; }
    public ICollection<Fornecedor.Fornecedor>? Fornecedores { get; private set; }
}