using Core.ValueObjects;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Resources;

namespace Urbamais.Domain.Entities.Supplier;

public class Supplier : CorporateEntity
{
    public ICollection<Equipment>? Equipments { get; private set; }

    protected Supplier()
    { }

    public Supplier(NameVO tradeName, NameVO corporateName, CnpjVO cnpj, string ie,
        string? im, List<Address> listAddress, List<Phone>? listPhone, List<Email>? listEmail)
        : base(tradeName, corporateName, cnpj, ie, im, listAddress, listPhone, listEmail)
    { }
}