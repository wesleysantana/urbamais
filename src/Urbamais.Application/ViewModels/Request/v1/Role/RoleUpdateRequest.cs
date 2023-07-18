namespace Urbamais.Application.ViewModels.Request.v1.Role;

public class RoleUpdateRequest
{
    public string? Name { get; set; }

    public IDictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
}