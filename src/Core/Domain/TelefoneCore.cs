using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public class TelefoneCore : BaseEntity, IEntity
{
    public string? Numero { get; private set; }

    public TelefoneCore(string numero)
    {
        Numero = numero.Trim();
        Validate(this, new TelefoneValidator());

        if (!IsValid && Id == default) Numero = default;
    }

    private class TelefoneValidator : AbstractValidator<TelefoneCore>
    {
        public TelefoneValidator()
        {
            RuleFor(x => x.Numero)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}