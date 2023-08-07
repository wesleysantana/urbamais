using Core.ValueObjects;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Resources;

namespace Urbamais.Domain.Entities.Supplier;

public class Supplier : PessoaJuridica
{
    public ICollection<Equipment>? Equipments { get; private set; }

    protected Supplier()
    { }

    public Supplier(string idUserCreation, Nome tradeName, Nome corporateName, Cnpj cnpj, string ie,
        string? im, List<Endereco> listAddress, List<Telefone>? listPhone, List<Email>? listEmail)
        : base(idUserCreation, tradeName, corporateName, cnpj, ie, im, listAddress, listPhone, listEmail)
    { }
}