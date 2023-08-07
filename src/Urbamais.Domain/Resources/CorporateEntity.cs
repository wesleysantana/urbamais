using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Domain.Resources;

public abstract class CorporateEntity : BaseEntity, IAggregateRoot
{
    private List<Phone>? _listPhone = new();
    private List<Email>? _listEmail = new();
    private List<Address> _listAddress = new();

    public Nome TradeName { get; private set; }
    public Nome CorporateName { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public string StateRegistration { get; private set; }
    public string? MunicipalRegistration { get; private set; }

    public IReadOnlyCollection<Address> Addresses
    {
        get => _listAddress;
        private set => _listAddress = value.ToList();
    }

    public IReadOnlyCollection<Phone> Phones
    {
        get => _listPhone!;
        private set => _listPhone = value.ToList();
    }

    public IReadOnlyCollection<Email> Emails
    {
        get => _listEmail!;
        private set => _listEmail = value.ToList();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected CorporateEntity()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public CorporateEntity(string idUserCreation, Nome tradeName, Nome corporateName, Cnpj cnpj, string stateRegistration,
        string? municipalRegistration, List<Address> listAddress, List<Phone>? listPhone, List<Email>? listEmail)
    {
        TradeName = tradeName;
        CorporateName = corporateName;
        Cnpj = cnpj;
        StateRegistration = stateRegistration;
        MunicipalRegistration = municipalRegistration;
        _listAddress = listAddress;
        _listPhone = listPhone;
        _listEmail = listEmail;

        Validate();

        if (IsValid)
        {
            IdUserCreation = idUserCreation;
        }
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(CorporateName.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(TradeName.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Cnpj.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Addresses.SelectMany(x => x.ValidationResult!.Errors));
        ValidationResult?.Errors.AddRange(Phones.SelectMany(x => x.ValidationResult!.Errors));
        ValidationResult?.Errors.AddRange(Emails.SelectMany(x => x.ValidationResult!.Errors));

        Validate(this, new CorporateEntityValidator());

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                if (item.Name.Equals(nameof(Addresses)))
                {
                    _listEmail = default;
                    continue;
                }

                if (item.Name.Equals(nameof(Phones)))
                {
                    _listEmail = default;
                    continue;
                }

                if (item.Name.Equals(nameof(Emails)))
                {
                    _listEmail = default;
                    continue;
                }

                item.SetValue(this, default);
            }
        }
    }

    public void Update(string idUserModification, Nome? corporateName = null, Nome? tradeName = null, Cnpj? cnpj = null,
        List<Address>? addresses = null, List<Phone>? phones = null, List<Email>? emails = null)
    {
        if (corporateName is not null) CorporateName = corporateName;
        if (tradeName is not null) TradeName = tradeName;
        if (cnpj is not null) Cnpj = cnpj;
        if (addresses is not null) Addresses = addresses;
        if (phones is not null) Phones = phones;
        if (emails is not null) Emails = emails;

        Validate();

        if (IsValid)
        {
            IdUserModification = idUserModification;
            ModificationDate = DateTime.Now;
        }
    }

    #region Sobrescrita Object

    public override string ToString() =>
        $"companie - Id: {Id}, Nome: {TradeName}, Razão Sociaol: {CorporateName}, Cnpj: {Cnpj}";

    public override bool Equals(object? obj)
    {
        return obj is CorporateEntity companie &&
            EqualityComparer<Nome>.Default.Equals(CorporateName, companie.CorporateName) &&
            EqualityComparer<Nome>.Default.Equals(TradeName, companie.TradeName) &&
            EqualityComparer<Cnpj>.Default.Equals(Cnpj, companie.Cnpj) &&
            Enumerable.SequenceEqual(_listAddress!.OrderBy(e => e.Id), companie._listAddress!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmail!.OrderBy(e => e.Id), companie._listEmail!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmail!.OrderBy(e => e.Id), companie._listEmail!.OrderBy(e => e.Id));
    }

    public override int GetHashCode()
    {
        int hashAddress = 0;
        foreach (var item in _listAddress!)
        {
            hashAddress += item.GetHashCode();
        }

        int hashPhone = 0;
        foreach (var item in _listEmail!)
        {
            hashPhone += item.GetHashCode();
        }

        int hashEmail = 0;
        foreach (var item in _listEmail!)
        {
            hashEmail += item.GetHashCode();
        }

        return HashCode.Combine(Id, CorporateName, TradeName, Cnpj, Addresses) + hashAddress + hashPhone + hashEmail;
    }

    public static bool operator ==(CorporateEntity left, CorporateEntity right) => left.Equals(right);

    public static bool operator !=(CorporateEntity left, CorporateEntity right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class CorporateEntityValidator : AbstractValidator<CorporateEntity>
    {
        public CorporateEntityValidator()
        {
            RuleFor(x => x.StateRegistration)
                .MaximumLength(50);

            RuleFor(x => x.MunicipalRegistration)
                .MaximumLength(50);

            RuleFor(x => x.Addresses)
                .NotEmpty();
        }
    }
}