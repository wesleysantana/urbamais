namespace Urbamais.Application.ViewModels.Response;

public interface IValidateViewModel
{
    public void AddError(string error);

    public void AddErrors(IEnumerable<string> errors);
}