using Core.Domain;
using Urbamais.Domain.Entities.Construction;
using Urbamais.Domain.Entities.Supplier;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Email : EmailCore
{
    public ICollection<Collaborator>? Collaborators { get; private set; }
    public ICollection<Companie>? Companies { get; private set; }
    public ICollection<Supplier.Supplier>? Suppliers { get; private set; }

    public Email(string idUserCreation, string address) : base(idUserCreation, address)
    {
    }
}