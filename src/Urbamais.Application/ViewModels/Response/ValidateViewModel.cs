namespace Urbamais.Application.ViewModels.Response;

public abstract class ValidateViewModel : IValidateViewModel
{
    public bool Success => Errors.Count == 0;

    public List<string> Errors { get; set; } = new List<string>();

    public void AddError(string error) => Errors.Add(error);

    public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
}