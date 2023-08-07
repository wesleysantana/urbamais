using Core.Domain;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Domain.Entities.Obras;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Telefone : TelefoneCore
{
    public ICollection<Colaborador>? Colaboradores { get; private set; }
    public ICollection<Empresa>? Empresas { get; private set; }
    public ICollection<Fornecedor>? Fornecedores { get; private set; }

    public Telefone(string idUserCreation, string numero) : base(idUserCreation, numero)
    {
    }
}