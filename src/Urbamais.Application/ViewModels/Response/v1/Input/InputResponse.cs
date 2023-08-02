using Urbamais.Application.ViewModels.Response.V1.Unit;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Application.ViewModels.Response.V1.Input;

public class InputResponse : ValidateViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public UnitResponse? Unit { get; set; }
    public InputType Type { get; set; }
}