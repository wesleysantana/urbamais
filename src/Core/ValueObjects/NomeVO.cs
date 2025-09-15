using Core.SeedWork;
using FluentValidation;

namespace Core.ValueObjects;

public sealed class NomeVO : BaseValidate
{
    public string Nome { get; private set; } = "";

    public NomeVO(string nome)
    {
        Nome = nome.Trim();
        Validate(this, new NomeValidator());

        if (!IsValid) Nome = "";
    }       

    private class NomeValidator : AbstractValidator<NomeVO>
    {
        public NomeValidator()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(3)
                .MaximumLength(255);
        }
    }
}