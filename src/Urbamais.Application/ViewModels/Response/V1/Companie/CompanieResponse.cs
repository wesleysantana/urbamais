using Urbamais.Application.ViewModels.Response.V1.Address;
using Urbamais.Application.ViewModels.Response.V1.Email;
using Urbamais.Application.ViewModels.Response.V1.Phone;

namespace Urbamais.Application.ViewModels.Response.V1.Companie;

public class CompanieResponse : ValidateViewModel
{
    public ICollection<PhoneResponse>? Phones { get; set; }
    public ICollection<EmailResponse>? Emails { get; set; }
    public ICollection<AddressResponse>? Addresses { get; set; }

    public string TradeName { get; set; } = string.Empty;
    public string CorporateName { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string? StateRegistration { get; set; }
    public string? MunicipalRegistration { get; set; }
}