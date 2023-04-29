using FluentValidation;
using System.Reflection;

namespace Urbamais.Domain.Entities.Core;

public class Endereco : BaseEntity, IEntity
{
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string Complemento { get; private set; }
    public string Bairro { get; private set; }
    public Cidade Cidade { get; private set; }

    public Endereco(string logradouro, string numero, string complemento, string bairro, Cidade cidade)
    {
        Logradouro = logradouro.Trim();
        Numero = numero.Trim();
        Complemento = complemento.Trim();
        Bairro = bairro.Trim();
        Cidade = cidade;

        Validate(this, new EnderecoValidator());

        ValidationResult.Errors.AddRange(Cidade.ValidationResult.Errors);

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    #region Sobrescrita Object

    public override string ToString() => $"Cidade - Id: {Id}, Logradouro: {Logradouro}, Número: {Numero}, " +
        $"Complemento: {Complemento}, Bairro: {Bairro}, Cidade: {Cidade.Nome}, Estado: {Cidade.Uf}";

    public override bool Equals(object? obj)
    {
        return obj is Endereco endereco &&
            Id == endereco.Id &&
            Logradouro == endereco.Logradouro &&
            Numero == endereco.Numero &&
            Complemento == endereco.Complemento &&
            Bairro == endereco.Bairro &&
            EqualityComparer<Cidade>.Default.Equals(Cidade, endereco.Cidade);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Logradouro, Numero, Complemento, Bairro, Cidade);
    }

    public static bool operator ==(Endereco left, Endereco right) => left.Equals(right);

    public static bool operator !=(Endereco left, Endereco right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(x => x.Logradouro)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Numero)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.Complemento)
                .MaximumLength(100);

            RuleFor(x => x.Bairro)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}