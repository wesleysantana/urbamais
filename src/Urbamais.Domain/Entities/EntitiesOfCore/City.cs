using Core.Domain;
using Core.ValueObjects;

namespace Urbamais.Domain.Entities.EntitiesOfCore;

public class City : CityCore
{
    public IEnumerable<Address>? Address { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected City()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public City(NameVO name, Uf uf) : base(name, uf)
    {
    }
}