using Core.Domain;
using Urbamais.Domain.Entities.Supplier;
using Urbamais.Domain.Entities.Construction;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Phone : PhoneCore
{
    public ICollection<Collaborator>? Collaborators { get; private set; }
    public ICollection<Companie>? Companies { get; private set; }
    public ICollection<Supplier.Supplier>? Suppliers { get; private set; }

    public Phone(string number) : base(number)
    {
    }
}