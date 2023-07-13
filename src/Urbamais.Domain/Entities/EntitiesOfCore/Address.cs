using Core.Domain;
using Urbamais.Domain.Entities.Construction;
using Urbamais.Domain.Entities.Supplier;
using Urbamais.Domain.Entities.Suprimento;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public sealed class Address : AddressCore
{
    public ICollection<Collaborator>? Collaborators { get; private set; }
    public ICollection<Companie>? Companies { get; private set; }
    public ICollection<Supplier.Supplier>? Supplier { get; private set; }
    public ICollection<Purchase>? Purchases { get; private set; }
    public new int CityId { get; private set; }
    public new City? City { get; private set; }

    public Address(string thoroughfare, string number, string complement, string neighborhood, string zipCode, int cityId)
        : base(thoroughfare, number, complement, neighborhood, zipCode, cityId)
    {
    }
}