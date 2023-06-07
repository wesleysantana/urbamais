using Core.Domain;
using Urbamais.Domain.Entities.Fornecedor;
using Urbamais.Domain.Entities.Obra;

namespace Urbamais.Domain.Entities.CoreRelationManyToMany;

public sealed class Telefone : TelefoneCore
{
    public ICollection<Colaborador>? Colaboradores { get; private set; }
    public ICollection<Empresa>? Empresas { get; private set; }
    public ICollection<Fornecedor.Fornecedor>? Fornecedores { get; private set; }

    public Telefone(string numero) : base(numero)
    {
    }
}