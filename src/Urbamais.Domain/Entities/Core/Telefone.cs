using FluentValidation;

namespace Urbamais.Domain.Entities.Core;

public sealed class Telefone : BaseEntity, IEntity
{
    public string? Numero { get; private set; }

    public Telefone(string numero)
    {
        Numero = numero.Trim();
        Validate(this, new TelefoneValidator());

        if (!IsValid) Numero = default;
    }

    #region Sobrescrita Object

    public override string ToString() => $"Telefone - Id: {Id}, Número: {Numero}";

    public override bool Equals(object? obj)
    {
        return obj is Telefone telefone &&
               Numero == telefone.Numero;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Numero);
    }

    #endregion Sobrescrita Object

    public static bool operator ==(Telefone left, Telefone right) => left.Equals(right);

    public static bool operator !=(Telefone left, Telefone right) => !left.Equals(right);

    private class TelefoneValidator : AbstractValidator<Telefone>
    {
        public TelefoneValidator()
        {
            RuleFor(x => x.Numero)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}