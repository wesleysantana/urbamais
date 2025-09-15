using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public abstract class EmailCore : BaseEntity, IEntity
{
    public string? Endereco { get; private set; }

    public EmailCore(string endereco)
    {
        Endereco = endereco.Trim();

        Validate(this, new EmailValidator());

        if (!IsValid && Id == default) Endereco = default;
    }    

    private class EmailValidator : AbstractValidator<EmailCore>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Endereco)
                .EmailAddress();

            RuleFor(x => x.Endereco)
                .MaximumLength(255);
        }
    }
}