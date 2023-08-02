using Urbamais.Application.ViewModels.Response.V1.City;

namespace Urbamais.Application.ViewModels.Response.V1.Address;

public class AddressResponse
{
    public string? Thoroughfare { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public string? ZipCode { get; set; }
    public CityResponse? City { get; set; }
}