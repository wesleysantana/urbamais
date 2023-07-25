using Core.Domain;
using Urbamais.Domain.Entities.Construction;
using Urbamais.Domain.Entities.Supplier;
using Urbamais.Domain.Entities.Supply;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Address : AddressCore
{
    public ICollection<Collaborator>? Collaborators { get; private set; }
    public ICollection<Companie>? Companies { get; private set; }
    public ICollection<Supplier.Supplier>? Supplier { get; private set; }
    public ICollection<Purchase>? Purchases { get; private set; }
    public new int CityId { get; private set; }
    public new City? City { get; private set; }

    public Address(string idUserCreation, string thoroughfare, string number, string complement, string neighborhood, string zipCode, int cityId)
        : base(idUserCreation, thoroughfare, number, complement, neighborhood, zipCode, cityId)
    {
    }
}