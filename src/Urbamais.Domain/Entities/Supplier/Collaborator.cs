using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Domain.Entities.Supplier;

public class Collaborator : BaseEntity, IAggregateRoot
{
    private List<Phone>? _listPhones = new();
    private List<Email>? _listEmails = new();
    private List<Address> _listAddress = new();

    public Nome Name { get; private set; }
    public Cpf Cpf { get; private set; }

    public string? NumberCTPS { get; private set; }
    public string? NumberCNH { get; private set; }
    public string? TypeCNH { get; private set; }
    public DateTime? ExpirationDateCNH { get; private set; }

    public string CNH { get; private set; }
    public string EPI { get; private set; }
    public string CTPS { get; private set; }
    public string? NumberAdmissionExam { get; private set; }

    public DateTime? ExpirationDateAdmissionExam { get; private set; }
    public string AdmissionExam { get; private set; }
    public string RegistrationForm { get; private set; }
    public string ServiceOrder { get; private set; }

    public IReadOnlyCollection<Address> Address
    {
        get => _listAddress;
        private set => _listAddress = value.ToList();
    }

    public IReadOnlyCollection<Phone> Phones
    {
        get => _listPhones!;
        private set => _listPhones = value.ToList();
    }

    public IReadOnlyCollection<Email> Emails
    {
        get => _listEmails!;
        private set => _listEmails = value.ToList();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Collaborator()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Collaborator(string idUserCreation, Nome name, Cpf cpf,
        List<Address> address, string numberCtps, string numberCNH, string typeCnh,
        DateTime expirationDateCNH, string cnh, string epi, string ctps,
        string numeroExameAdmissional, DateTime validadeExameAdmissional, string admissionExam,
        string registrationForm, string serviceOrder, List<Phone>? listPhones, List<Email>? listEmails)
    {
        Name = name;
        Cpf = cpf;
        Address = address;
        NumberCTPS = numberCtps;
        NumberCNH = numberCNH;
        TypeCNH = typeCnh;
        ExpirationDateCNH = expirationDateCNH;
        CNH = cnh;
        EPI = epi;
        CTPS = ctps;
        NumberAdmissionExam = numeroExameAdmissional;
        ExpirationDateAdmissionExam = validadeExameAdmissional;
        AdmissionExam = admissionExam;
        RegistrationForm = registrationForm;
        ServiceOrder = serviceOrder;
        _listPhones = listPhones;
        _listEmails = listEmails;

        Validate();

        if (IsValid)
            IdUserCreation = idUserCreation;
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Name.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Cpf.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Address.SelectMany(x => x.ValidationResult!.Errors));
        ValidationResult?.Errors.AddRange(Phones.SelectMany(x => x.ValidationResult!.Errors));
        ValidationResult?.Errors.AddRange(Emails.SelectMany(x => x.ValidationResult!.Errors));

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                if (item.Name.Equals(nameof(Address)))
                {
                    _listPhones = default;
                    continue;
                }

                if (item.Name.Equals(nameof(Phones)))
                {
                    _listPhones = default;
                    continue;
                }

                if (item.Name.Equals(nameof(Emails)))
                {
                    _listEmails = default;
                    continue;
                }

                item.SetValue(this, default);
            }
        }
    }

    public void Update(string idUserModification, Nome? name = null, Cpf? cpf = null,
       List<Address>? address = null, string? numberCtps = null, string? numbercnh = null, string? typeCnh = null,
       DateTime? expirationDateCNH = null, string? cnh = null, string? epi = null, string? ctps = null,
       string? numberAdmissionExam = null, DateTime? expirationDateAdmissionExam = null,
       string? admissionExam = null, string? registrationForm = null, string? serviceOrder = null,
       List<Phone>? listPhones = null, List<Email>? listEmails = null)
    {
        var memento = CreateMemento();

        if (name is not null) Name = name;
        if (cpf is not null) Cpf = cpf;
        if (address is not null) Address = address;
        if (numberCtps is not null) NumberCTPS = numberCtps;
        if (numbercnh is not null) NumberCNH = numbercnh;
        if (typeCnh is not null) TypeCNH = typeCnh;
        if (expirationDateCNH is not null) ExpirationDateCNH = (DateTime)expirationDateCNH;
        if (cnh is not null) CNH = cnh;
        if (epi is not null) EPI = epi;
        if (ctps is not null) CTPS = ctps;

        if (numberAdmissionExam is not null)
            NumberAdmissionExam = numberAdmissionExam;

        if (expirationDateAdmissionExam is not null)
            ExpirationDateAdmissionExam = (DateTime)expirationDateAdmissionExam;

        if (admissionExam is not null) AdmissionExam = admissionExam;
        if (registrationForm is not null) RegistrationForm = registrationForm;
        if (serviceOrder is not null) ServiceOrder = serviceOrder;
        if (listPhones is not null) _listPhones = listPhones;
        if (listEmails is not null) _listEmails = listEmails;

        Validate();

        if (IsValid)
        {
            IdUserModification = idUserModification;
            ModificationDate = DateTime.Now;
        }
        else
            RestoreMemento(memento);
    }

    #region memento

    private object CreateMemento()
    {
        return new
        {
            Name,
            Cpf,
            Address,
            NumberCTPS,
            NumberCNH,
            TypeCNH,
            ExpirationDateCNH,
            CNH,
            EPI,
            CTPS,
            NumberAdmissionExam,
            ExpirationDateAdmissionExam,
            AdmissionExam,
            RegistrationForm,
            ServiceOrder,
            _listPhones,
            _listEmails
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Name = state.Name;
        Cpf = state.Cpf;
        Address = state.Address;
        NumberCTPS = state.NumberCTPS;
        NumberCNH = state.NumberCNH;
        TypeCNH = state.TypeCNH;
        ExpirationDateCNH = state.ExpirationDateCNH;
        CNH = state.CNH;
        EPI = state.EPI;
        CTPS = state.CTPS;
        NumberAdmissionExam = state.NumberAdmissionExam;
        ExpirationDateAdmissionExam = state.ExpirationDateAdmissionExam;
        AdmissionExam = state.AdmissionExam;
        RegistrationForm = state.RegistrationForm;
        ServiceOrder = state.ServiceOrder;
        _listPhones = state._listPhones;
        _listEmails = state._listEmails;
    }

    #endregion memento

    #region Sobrescrita Object

    public override string ToString()
    {
        return $"Colaborador - Id: {Id}, Nome: {Name}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Collaborator colaborador &&
            EqualityComparer<Nome>.Default.Equals(Name, colaborador.Name) &&
            EqualityComparer<Cpf>.Default.Equals(Cpf, colaborador.Cpf) &&
            NumberCTPS == colaborador.NumberCTPS &&
            NumberCNH == colaborador.NumberCNH &&
            TypeCNH == colaborador.TypeCNH &&
            ExpirationDateCNH == colaborador.ExpirationDateCNH &&
            EqualityComparer<string>.Default.Equals(CNH, colaborador.CNH) &&
            EqualityComparer<string>.Default.Equals(EPI, colaborador.EPI) &&
            EqualityComparer<string>.Default.Equals(CTPS, colaborador.CTPS) &&
            NumberAdmissionExam == colaborador.NumberAdmissionExam &&
            ExpirationDateAdmissionExam == colaborador.ExpirationDateAdmissionExam &&
            EqualityComparer<string>.Default.Equals(AdmissionExam, colaborador.AdmissionExam) &&
            EqualityComparer<string>.Default.Equals(RegistrationForm, colaborador.RegistrationForm) &&
            EqualityComparer<string>.Default.Equals(ServiceOrder, colaborador.ServiceOrder) &&
            Enumerable.SequenceEqual(_listAddress!.OrderBy(e => e.Id), colaborador._listAddress!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listPhones!.OrderBy(e => e.Id), colaborador._listPhones!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmails!.OrderBy(e => e.Id), colaborador._listEmails!.OrderBy(e => e.Id));
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(_listPhones);
        hash.Add(_listEmails);
        hash.Add(Name);
        hash.Add(Cpf);
        hash.Add(Address);
        hash.Add(NumberCTPS);
        hash.Add(NumberCNH);
        hash.Add(TypeCNH);
        hash.Add(ExpirationDateCNH);
        hash.Add(CNH);
        hash.Add(EPI);
        hash.Add(CTPS);
        hash.Add(NumberAdmissionExam);
        hash.Add(ExpirationDateAdmissionExam);
        hash.Add(AdmissionExam);
        hash.Add(RegistrationForm);
        hash.Add(ServiceOrder);
        return hash.ToHashCode();
    }

    #endregion Sobrescrita Object

    private class ColaboradorValidator : AbstractValidator<Collaborator>
    {
        public ColaboradorValidator()
        {
            RuleFor(x => x.NumberCTPS)
                .MaximumLength(25);

            RuleFor(x => x.NumberCNH)
                .Length(9);

            RuleFor(x => x.TypeCNH)
                .MaximumLength(2);

            RuleFor(x => x.NumberAdmissionExam)
                .MaximumLength(20);
        }
    }
}