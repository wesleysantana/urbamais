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

    #region Sobrescrita Object

    public override string ToString() => $"Email: {Endereco}";

    public override bool Equals(object? obj)
    {
        return obj is EmailCore email &&
            Id == email.Id &&
            Endereco == email.Endereco;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Endereco);
    }

    public static bool operator ==(EmailCore left, EmailCore right) => left.Equals(right);

    public static bool operator !=(EmailCore left, EmailCore right) => !left.Equals(right);

    #endregion Sobrescrita Object

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