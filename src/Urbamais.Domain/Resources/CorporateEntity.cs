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

    public NameVO TradeName { get; private set; }
    public NameVO CorporateName { get; private set; }
    public CnpjVO Cnpj { get; private set; }
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

    public CorporateEntity(NameVO tradeName, NameVO corporateName, CnpjVO cnpj, string stateRegistration,
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

    public void Update(NameVO? razaoSocial = null, NameVO? nomeFantasia = null, CnpjVO? cnpj = null,
        List<Address>? enderecos = null, List<Phone>? telefones = null, List<Email>? emails = null)
    {
        if (razaoSocial is not null) CorporateName = razaoSocial;
        if (nomeFantasia is not null) TradeName = nomeFantasia;
        if (cnpj is not null) Cnpj = cnpj;
        if (enderecos is not null) Addresses = enderecos;
        if (telefones is not null) Phones = telefones;
        if (emails is not null) Emails = emails;

        Validate();
    }

    #region Sobrescrita Object

    public override string ToString() =>
        $"companie - Id: {Id}, Nome: {TradeName}, Razão Sociaol: {CorporateName}, Cnpj: {Cnpj}";

    public override bool Equals(object? obj)
    {
        return obj is CorporateEntity companie &&
            EqualityComparer<NameVO>.Default.Equals(CorporateName, companie.CorporateName) &&
            EqualityComparer<NameVO>.Default.Equals(TradeName, companie.TradeName) &&
            EqualityComparer<CnpjVO>.Default.Equals(Cnpj, companie.Cnpj) &&
            Enumerable.SequenceEqual(_listAddress!.OrderBy(e => e.Id), companie._listAddress!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmail!.OrderBy(e => e.Id), companie._listEmail!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmail!.OrderBy(e => e.Id), companie._listEmail!.OrderBy(e => e.Id));
    }

    public override int GetHashCode()
    {
        int hashEndereco = 0;
        foreach (var item in _listAddress!)
        {
            hashEndereco += item.GetHashCode();
        }

        int hashTel = 0;
        foreach (var item in _listEmail!)
        {
            hashTel += item.GetHashCode();
        }

        int hashEmail = 0;
        foreach (var item in _listEmail!)
        {
            hashEmail += item.GetHashCode();
        }

        return HashCode.Combine(Id, CorporateName, TradeName, Cnpj, Addresses) + hashEndereco + hashTel + hashEmail;
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