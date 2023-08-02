namespace Urbamais.Application.ViewModels.Response.V1.Unit;

public class UnitResponse : ValidateViewModel
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Acronym { get; set; }   
}