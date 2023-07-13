using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;
using System.Reflection;

namespace Core.Domain;

public abstract class AddressCore : BaseEntity, IEntity
{
    public string Thoroughfare { get; private set; }
    public string Number { get; private set; }
    public string? Complement { get; private set; }
    public string Neighborhood { get; private set; }
    public string ZipCode { get; private set; }
    public int CityId { get; private set; }
    public virtual CityCore? City { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected AddressCore()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public AddressCore(string thoroughfare, string number, string complement, string zipCode, string neighborhood, int cityId)
    {
        Thoroughfare = thoroughfare.Trim();
        Number = number.Trim();
        Complement = complement.Trim();
        ZipCode = zipCode.Trim();
        Neighborhood = neighborhood.Trim();
        CityId = cityId;

        Validate(this, new AddressValidator());

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

    public override string ToString() => $"Cidade - Id: {Id}, Logradouro: {Thoroughfare}, Número: {Number}, " +
        $"Complemento: {Complement}, Bairro: {Neighborhood}, Cep: {ZipCode}, Cidade: {City?.Name}, Estado: {City?.Uf}";

    public override bool Equals(object? obj)
    {
        return obj is AddressCore endereco &&
            Id == endereco.Id &&
            Thoroughfare == endereco.Thoroughfare &&
            Number == endereco.Number &&
            Complement == endereco.Complement &&
            ZipCode == endereco.ZipCode &&
            Neighborhood == endereco.Neighborhood &&
            EqualityComparer<CityCore>.Default.Equals(City, endereco.City);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Thoroughfare, Number, Complement, ZipCode, Neighborhood, City);
    }

    public static bool operator ==(AddressCore left, AddressCore right) => left.Equals(right);

    public static bool operator !=(AddressCore left, AddressCore right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class AddressValidator : AbstractValidator<AddressCore>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Thoroughfare)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Number)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.Complement)
                .MaximumLength(100);

            RuleFor(x => x.Neighborhood)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .Length(8);

            RuleFor(x => x.CityId)
                .GreaterThan(0);
        }
    }
}