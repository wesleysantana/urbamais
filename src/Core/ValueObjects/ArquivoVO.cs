using Core.Enums;
using Core.SeedWork;
using FluentValidation;
using System.Reflection;

namespace Core.ValueObjects;

public sealed class ArquivoVO : BaseValidate
{
    public Guid Id { get; private set; }
    public TipoArquivo TipoArquivo { get; private set; }
    public string Extensao { get; private set; } = string.Empty;

    public ArquivoVO(TipoArquivo tipoArquivo, string extensao)
    {
        Id = new Guid();
        TipoArquivo = tipoArquivo;
        Extensao = extensao;

        Validate(this, new ArquivoValidator());

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    private class ArquivoValidator : AbstractValidator<ArquivoVO>
    {
        public ArquivoValidator()
        {
            RuleFor(x => x.TipoArquivo)
                .IsInEnum();

            RuleFor(x => x.Extensao)
                .NotNull()
                .NotEmpty();
        }
    }
}