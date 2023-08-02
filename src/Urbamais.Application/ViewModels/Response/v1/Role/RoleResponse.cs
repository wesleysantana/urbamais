namespace Urbamais.Application.ViewModels.Response.V1.Role;

public class RoleResponse
{
    public bool Success { get; private set; }

    public List<string> Errors { get; private set; }

    public RoleResponse() => Errors = new List<string>();

    public RoleResponse(bool success = true) : this() => Success = success;

    public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
}