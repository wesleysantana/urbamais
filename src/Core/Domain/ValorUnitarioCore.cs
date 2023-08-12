using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Core.Domain;

public class ValorUnitarioCore : BaseValidate, IEntity
{
    public decimal Valor { get; set; }

    private class ValorUnitarioCoreValidator : AbstractValidator<ValorUnitarioCore>
    {
        public ValorUnitarioCoreValidator()
        {
            RuleFor(x => x.Valor)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}