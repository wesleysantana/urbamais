using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Application.ViewModels.Response.v1.Input;

public class InputResponse
{
    public string? Name { get;  set; }
    public string? Description { get; set; }
    public int UnitId { get;  set; }
    public InputType Type { get;  set; }
}