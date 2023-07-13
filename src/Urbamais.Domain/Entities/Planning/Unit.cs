using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Urbamais.Domain.Entities.Planning;

public class Unit : BaseEntity, IAggregateRoot
{
    public string? Description { get; private set; }
    public string? Acronym { get; private set; }
    public virtual ICollection<Input>? Inputs { get; private set; }

    public Unit(string? description, string? acronym)
    {
        Description = description?.Trim();
        Acronym = acronym?.Trim();

        Validate();
    }

    private void Validate()
    {
        Validate(this, new UnitValidator());

        if (!IsValid && Id == 0)
        {
            Description = default;
            Acronym = default;
        }
    }

    public void Update(string? description = null, string? acronym = null)
    {
        if (!string.IsNullOrWhiteSpace(description)) Description = description.Trim();
        if (!string.IsNullOrWhiteSpace(acronym)) Acronym = acronym.Trim();

        Validate();

        if(IsValid) ModificationDate = DateTime.Now;
    }

    #region Sobrescrita Object

    public override string ToString() =>
        $"Unidade - Id: {Id}, Descricao: {Description}, Sigla: {Acronym}";

    public override bool Equals(object? obj)
    {
        return obj is Unit unidade &&
            Id == unidade.Id &&
            Description == unidade.Description &&
            Acronym == unidade.Acronym;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Description, Acronym);
    }

    public static bool operator ==(Unit left, Unit right) => left.Equals(right);

    public static bool operator !=(Unit left, Unit right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class UnitValidator : AbstractValidator<Unit>
    {
        public UnitValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Acronym)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}