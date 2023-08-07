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
    public string CodigoPostal { get; private set; }
    public int CidadeId { get; private set; }
    public virtual CidadeCore? Cidade { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected EnderecoCore()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public EnderecoCore(string idUserCreation, string logradouro, string numero, string complemento, 
        string codigoPostal, string bairro, int cidadeId)
    {
        Logradouro = logradouro.Trim();
        Numero = numero.Trim();
        Complemento = complemento.Trim();
        CodigoPostal = codigoPostal.Trim();
        Bairro = bairro.Trim();
        CidadeId = cidadeId;

        Validate(this, new EnderecoValidator());

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
        else
            IdUserCreation = idUserCreation;
    }

    public void Update(string idUserModification, string? logradouro = null, string? numero = null, string? complemento = null,
        string? codigoPostal = null, string? bairro = null, int? cidadeId = null)
    {
        var memento = CreateMemento();

        if (!string.IsNullOrWhiteSpace(logradouro)) Logradouro = logradouro.Trim();
        if (!string.IsNullOrWhiteSpace(numero)) Numero = numero.Trim();
        if (!string.IsNullOrWhiteSpace(complemento)) Complemento = complemento.Trim();
        if (!string.IsNullOrWhiteSpace(codigoPostal)) CodigoPostal = codigoPostal.Trim();
        if (!string.IsNullOrWhiteSpace(bairro)) Bairro = bairro.Trim();
        if (cidadeId != null) CidadeId = (int)cidadeId;

        Validate(this, new EnderecoValidator());

        if (IsValid)
        {
            IdUserModification = idUserModification;
            ModificationDate = DateTime.Now;
        }
        else
            RestoreMemento(memento);
    }

    #region Memento

    private object CreateMemento()
    {
        return new
        {
            Logradouro,
            Numero,
            Complemento,
            CodigoPostal,
            Bairro,
            CidadeId
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Logradouro = state.Thoroughfare;
        Numero = state.Number;
        Complemento = state.Complement;
        CodigoPostal = state.ZipCode;
        Bairro = state.Neighborhood;
        CidadeId = state.CityId;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() => $"Cidade - Id: {Id}, Logradouro: {Logradouro}, Número: {Numero}, " +
        $"Complemento: {Complemento}, Bairro: {Bairro}, Cep: {CodigoPostal}, Cidade: {Cidade?.Nome}, Estado: {Cidade?.Uf}";

    public override bool Equals(object? obj)
    {
        return obj is EnderecoCore endereco &&
            Id == endereco.Id &&
            Logradouro == endereco.Logradouro &&
            Numero == endereco.Numero &&
            Complemento == endereco.Complemento &&
            CodigoPostal == endereco.CodigoPostal &&
            Bairro == endereco.Bairro &&
            EqualityComparer<CidadeCore>.Default.Equals(Cidade, endereco.Cidade);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Logradouro, Numero, Complemento, CodigoPostal, Bairro, Cidade);
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

            RuleFor(x => x.CodigoPostal)
                .NotEmpty()
                .Length(8);

            RuleFor(x => x.CidadeId)
                .GreaterThan(0);
        }
    }
}