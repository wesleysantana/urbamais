namespace Urbamais.Application.ViewModels.Response.V1.Perfil;

public class PerfilResponse
{
    public bool Success { get; private set; }

    public List<string> Errors { get; private set; }

    public PerfilResponse() => Errors = new List<string>();

    public PerfilResponse(bool success = true) : this() => Success = success;

    public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
    
}