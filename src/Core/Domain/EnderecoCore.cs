using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;
using System.Reflection;

namespace Core.Domain;

public abstract class EnderecoCore : BaseEntity, IEntity
{
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string? Complemento { get; private set; }
    public string Bairro { get; private set; }
    public string Cep { get; private set; }
    public int CidadeId { get; private set; }
    public virtual CidadeCore? Cidade { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected EnderecoCore()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public EnderecoCore(string logradouro, string numero, string complemento, string cep, string bairro, int cidadeId)
    {
        Logradouro = logradouro.Trim();
        Numero = numero.Trim();
        Complemento = complemento.Trim();
        Cep = cep.Trim();
        Bairro = bairro.Trim();
        CidadeId = cidadeId;

        Validate(this, new EnderecoValidator());

        if (!IsValid && Id == default)
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
        $"Complemento: {Complemento}, Bairro: {Bairro}, Cep: {Cep}, Cidade: {Cidade?.Nome}, Estado: {Cidade?.Uf}";

    public override bool Equals(object? obj)
    {
        return obj is EnderecoCore endereco &&
            Id == endereco.Id &&
            Logradouro == endereco.Logradouro &&
            Numero == endereco.Numero &&
            Complemento == endereco.Complemento &&
            Cep == endereco.Cep &&
            Bairro == endereco.Bairro &&
            EqualityComparer<CidadeCore>.Default.Equals(Cidade, endereco.Cidade);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Logradouro, Numero, Complemento, Cep, Bairro, Cidade);
    }

    public static bool operator ==(EnderecoCore left, EnderecoCore right) => left.Equals(right);

    public static bool operator !=(EnderecoCore left, EnderecoCore right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class EnderecoValidator : AbstractValidator<EnderecoCore>
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

            RuleFor(x => x.Cep)
                .NotEmpty()
                .Length(8);

            RuleFor(x => x.CidadeId)
                .GreaterThan(0);
        }
    }
}