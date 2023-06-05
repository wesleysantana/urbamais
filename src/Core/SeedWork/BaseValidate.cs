using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.SeedWork;

public class BaseValidate
{
    private bool _isValid = true;

    [NotMapped]
    public bool IsValid
    {
        get => !ValidationResult.Errors.Any() && _isValid;
        private set => _isValid = value;       
    }

    [NotMapped]
    public ValidationResult ValidationResult { get; private set; } = new();

    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);
        return IsValid = ValidationResult.IsValid;
    }
}