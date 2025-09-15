using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.SeedWork;

/*
public class BaseValidate
{
    private bool _isValid = true;

    [NotMapped]
    public bool IsValid
    {
        get => ValidationResult is not null && _isValid;
        set => _isValid = value;       
    }

    [NotMapped]
    public ValidationResult? ValidationResult { get; private set; }

    public void Validate<TModel>(TModel model, AbstractValidator<TModel>? validator = null)
    {
        if (validator is null)
        {
            ValidationResult = new ValidationResult();
            return;
        }

        var vr = validator.Validate(model);

        if (ValidationResult is null)
            ValidationResult = vr;
        else if (vr.Errors.Count > 0)
            ValidationResult.Errors.AddRange(vr.Errors);

        IsValid = ValidationResult.Errors.Count == 0;
    }
}
*/

public class BaseValidate
{
    // Remova o campo _isValid. Deixe IsValid derivar do ValidationResult.
    [NotMapped]
    public ValidationResult? ValidationResult { get; private set; }

    [NotMapped]
    public bool IsValid => ValidationResult?.IsValid ?? true;

    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);
        return IsValid; // usa IsValid derivado
    }

    // ✅ Helpers protegidos para agregar erros de outros componentes (VOs, filhos etc.)
    protected void AddErrorsFrom(BaseValidate? component)
    {
        if (component?.ValidationResult is null) return;
        if (component.ValidationResult.Errors.Count == 0) return;

        ValidationResult ??= new ValidationResult();
        ValidationResult.Errors.AddRange(component.ValidationResult.Errors);
        // IsValid é derivado, não precisa reatribuir nada aqui.
    }

    protected void AddErrorsFrom(IEnumerable<BaseValidate>? components)
    {
        if (components is null) return;
        foreach (var c in components)
            AddErrorsFrom(c);
    }
}