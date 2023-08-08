namespace Urbamais.Application.ViewModels.Request.V1.Perfil;

public class PerfilUpdateRequest
{
    public string? Name { get; set; }

    public IDictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
}