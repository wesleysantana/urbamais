using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.SeedWork;

public class BaseValidate
{    
    [NotMapped]
    public ValidationResult? ValidationResult { get; private set; }

    [NotMapped]
    public bool IsValid => ValidationResult?.IsValid ?? true;

    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);
        return IsValid; 
    }
   
    protected void AddErrorsFrom(BaseValidate? component)
    {
        if (component?.ValidationResult is null) return;
        if (component.ValidationResult.Errors.Count == 0) return;

        ValidationResult ??= new ValidationResult();
        ValidationResult.Errors.AddRange(component.ValidationResult.Errors);
    }

    protected void AddErrorsFrom(IEnumerable<BaseValidate>? components)
    {
        if (components is null) return;
        foreach (var c in components)
            AddErrorsFrom(c);
    }
}