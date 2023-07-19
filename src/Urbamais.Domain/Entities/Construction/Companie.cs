using Core.ValueObjects;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Resources;

namespace Urbamais.Domain.Entities.Construction;

public class Companie : CorporateEntity
{
    public ICollection<Construction>? Constructions { get; private set; }

    protected Companie() { }

    public Companie(string idUserCreation, NameVO tradeName, NameVO corporateName, CnpjVO cnpj, string ie,
        string? im, List<Address> listAddress, List<Phone>? listPhone, List<Email>? listEmail)
        : base(idUserCreation, tradeName, corporateName, cnpj, ie, im, listAddress, listPhone, listEmail) { }
}