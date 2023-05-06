using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public sealed class Email : BaseEntity, IEntity
{
    public string? Endereco { get; private set; }

    public Email(string endereco)
    {
        Endereco = endereco.Trim();

        Validate(this, new EmailValidator());

        if (!IsValid) Endereco = default;
    }

    #region Sobrescrita Object

    public override string ToString() => $"Email: {Endereco}";

    public override bool Equals(object? obj)
    {
        return obj is Email email &&
            Id == email.Id &&
            Endereco == email.Endereco;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Endereco);
    }

    public static bool operator ==(Email left, Email right) => left.Equals(right);

    public static bool operator !=(Email left, Email right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Endereco)
                .EmailAddress();
        }
    }
}