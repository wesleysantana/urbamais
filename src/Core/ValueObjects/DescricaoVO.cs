using Core.SeedWork;
using FluentValidation;

namespace Core.ValueObjects;

public sealed class DescricaoVO : BaseValidate
{
    public string? Descricao { get; private set; }

    public DescricaoVO(string descricao)
    {
        Descricao = descricao.Trim();
        Validate(this, new DescricaoValidator());

        if (!IsValid) Descricao = default;
    }

    private class DescricaoValidator : AbstractValidator<DescricaoVO>
    {
        public DescricaoValidator()
        {
            RuleFor(x => x.Descricao)
                .MaximumLength(255);
        }
    }
}