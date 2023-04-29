using FluentValidation;
using FluentValidation.Results;

namespace Urbamais.Domain.SeedWork;

public class BaseValidate
{
    private bool _isValid = true;

    public bool IsValid
    {
        get => !ValidationResult.Errors.Any() && _isValid;
        private set => _isValid = value;       
    }

    public ValidationResult ValidationResult { get; private set; } = new();

    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);
        return IsValid = ValidationResult.IsValid;
    }
}