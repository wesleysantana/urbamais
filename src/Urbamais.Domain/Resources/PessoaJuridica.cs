using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Domain.Resources;

public abstract class PessoaJuridica : BaseEntity, IAggregateRoot
{
    private List<Telefone>? _listTelefone = new();
    private List<Email>? _listEmail = new();
    private List<Endereco> _listEndereco = new();

    public Nome NomeFantasia { get; private set; }
    public Nome RazaoSocial { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public string InscricaoEstadual { get; private set; }
    public string? IncricaoMunicipal { get; private set; }

    public IReadOnlyCollection<Endereco> Enderecos
    {
        get => _listEndereco;
        private set => _listEndereco = value.ToList();
    }

    public IReadOnlyCollection<Telefone> Telefones
    {
        get => _listTelefone!;
        private set => _listTelefone = value.ToList();
    }

    public IReadOnlyCollection<Email> Emails
    {
        get => _listEmail!;
        private set => _listEmail = value.ToList();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected PessoaJuridica()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public PessoaJuridica(string idUserCreation, Nome nomeFantasia, Nome razaoSocial, Cnpj cnpj, string inscricaoEstadual,
        string? inscricaoMunicipal, List<Endereco> listEndereco, List<Telefone>? listTelefone, List<Email>? listEmail)
    {
        NomeFantasia = nomeFantasia;
        RazaoSocial = razaoSocial;
        Cnpj = cnpj;
        InscricaoEstadual = inscricaoEstadual;
        IncricaoMunicipal = inscricaoMunicipal;
        _listEndereco = listEndereco;
        _listTelefone = listTelefone;
        _listEmail = listEmail;

        Validate();

        if (IsValid)
        {
            IdUserCreation = idUserCreation;
        }
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(RazaoSocial.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(NomeFantasia.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Cnpj.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Enderecos.SelectMany(x => x.ValidationResult!.Errors));
        ValidationResult?.Errors.AddRange(Telefones.SelectMany(x => x.ValidationResult!.Errors));
        ValidationResult?.Errors.AddRange(Emails.SelectMany(x => x.ValidationResult!.Errors));

        Validate(this, new PessoaJuridicaValidator());

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                if (item.Name.Equals(nameof(Enderecos)))
                {
                    _listEmail = default;
                    continue;
                }

                if (item.Name.Equals(nameof(Telefones)))
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

    public void Update(string idUserModification, Nome? nomeFantasia = null, Nome? razaoSocial = null, Cnpj? cnpj = null,
        List<Endereco>? enderecos = null, List<Telefone>? telefones = null, List<Email>? emails = null)
    {
        if (nomeFantasia is not null) RazaoSocial = nomeFantasia;
        if (razaoSocial is not null) NomeFantasia = razaoSocial;
        if (cnpj is not null) Cnpj = cnpj;
        if (enderecos is not null) Enderecos = enderecos;
        if (telefones is not null) Telefones = telefones;
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
        $"companie - Id: {Id}, Nome: {NomeFantasia}, Razão Sociaol: {RazaoSocial}, Cnpj: {Cnpj}";

    public override bool Equals(object? obj)
    {
        return obj is PessoaJuridica companie &&
            EqualityComparer<Nome>.Default.Equals(RazaoSocial, companie.RazaoSocial) &&
            EqualityComparer<Nome>.Default.Equals(NomeFantasia, companie.NomeFantasia) &&
            EqualityComparer<Cnpj>.Default.Equals(Cnpj, companie.Cnpj) &&
            Enumerable.SequenceEqual(_listEndereco!.OrderBy(e => e.Id), companie._listEndereco!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmail!.OrderBy(e => e.Id), companie._listEmail!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmail!.OrderBy(e => e.Id), companie._listEmail!.OrderBy(e => e.Id));
    }

    public override int GetHashCode()
    {
        int hashAddress = 0;
        foreach (var item in _listEndereco!)
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

        return HashCode.Combine(Id, RazaoSocial, NomeFantasia, Cnpj, Enderecos) + hashAddress + hashPhone + hashEmail;
    }

    public static bool operator ==(PessoaJuridica left, PessoaJuridica right) => left.Equals(right);

    public static bool operator !=(PessoaJuridica left, PessoaJuridica right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class PessoaJuridicaValidator : AbstractValidator<PessoaJuridica>
    {
        public PessoaJuridicaValidator()
        {
            RuleFor(x => x.InscricaoEstadual)
                .MaximumLength(50);

            RuleFor(x => x.IncricaoMunicipal)
                .MaximumLength(50);

            RuleFor(x => x.Enderecos)
                .NotEmpty();
        }
    }
}