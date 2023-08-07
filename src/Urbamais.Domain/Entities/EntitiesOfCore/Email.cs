using Core.Domain;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.Entities.Fornecedores;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Email : EmailCore
{
    public ICollection<Colaborador>? Colaboradores { get; private set; }
    public ICollection<Empresa>? Empresas { get; private set; }
    public ICollection<Fornecedor>? Fornecedores { get; private set; }

    public Email(string idUserCreation, string endereco) : base(idUserCreation, endereco)
    {
    }
}