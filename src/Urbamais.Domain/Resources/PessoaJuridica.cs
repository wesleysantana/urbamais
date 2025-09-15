using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Domain.Resources;

public abstract class PessoaJuridica : BaseEntity, IAggregateRoot
{
    private List<Telefone>? _listTelefones = [];
    private List<Email>? _listEmails = [];
    private List<Endereco> _listEnderecos = [];

    public NomeVO NomeFantasia { get; private set; }
    public NomeVO RazaoSocial { get; private set; }
    public CnpjVO Cnpj { get; private set; }
    public string InscricaoEstadual { get; private set; }
    public string? InscricaoMunicipal { get; private set; }

    public IReadOnlyCollection<Endereco> Enderecos
    {
        get => _listEnderecos;
        private set => _listEnderecos = value.ToList();
    }

    public IReadOnlyCollection<Telefone> Telefones
    {
        get => _listTelefones!;
        private set => _listTelefones = value.ToList();
    }

    public IReadOnlyCollection<Email> Emails
    {
        get => _listEmails!;
        private set => _listEmails = value.ToList();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected PessoaJuridica()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public PessoaJuridica(NomeVO nomeFantasia, NomeVO razaoSocial, CnpjVO cnpj, string inscricaoEstadual,
        string? inscricaoMunicipal, List<Endereco> listEndereco, List<Telefone>? listTelefone, List<Email>? listEmail)
    {
        NomeFantasia = nomeFantasia;
        RazaoSocial = razaoSocial;
        Cnpj = cnpj;
        InscricaoEstadual = inscricaoEstadual;
        InscricaoMunicipal = inscricaoMunicipal;
        _listEnderecos = listEndereco;
        _listTelefones = listTelefone;
        _listEmails = listEmail;

        Validar();
    }

    private void Validar()
    {
        //ValidationResult?.Errors.AddRange(RazaoSocial.ValidationResult!.Errors);
        //ValidationResult?.Errors.AddRange(NomeFantasia.ValidationResult!.Errors);
        //ValidationResult?.Errors.AddRange(Cnpj.ValidationResult!.Errors);
        //ValidationResult?.Errors.AddRange(Enderecos.SelectMany(x => x.ValidationResult!.Errors));
        //ValidationResult?.Errors.AddRange(Telefones.SelectMany(x => x.ValidationResult!.Errors));
        //ValidationResult?.Errors.AddRange(Emails.SelectMany(x => x.ValidationResult!.Errors));

        //Validate(this, new PessoaJuridicaValidator());

        //if (!IsValid && Id == default)
        //{
        //    var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        //    foreach (var item in propriedades)
        //    {
        //        if (item.Name.Equals(nameof(Enderecos)))
        //        {
        //            _listTelefones = default;
        //            continue;
        //        }

        //        if (item.Name.Equals(nameof(Telefones)))
        //        {
        //            _listTelefones = default;
        //            continue;
        //        }

        //        if (item.Name.Equals(nameof(Emails)))
        //        {
        //            _listEmails = default;
        //            continue;
        //        }

        //        item.SetValue(this, default);
        //    }
        //}

        Validate(this, new PessoaJuridicaValidator());

        AddErrorsFrom(RazaoSocial);
        AddErrorsFrom(NomeFantasia);
        AddErrorsFrom(Cnpj);
        AddErrorsFrom(Enderecos);
        AddErrorsFrom(Telefones);
        AddErrorsFrom(Emails);

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var p in propriedades)
                p.SetValue(this, default);
        }
    }

    public void Update(NomeVO? razaoSocial = null, NomeVO? nomeFantasia = null, CnpjVO? cnpj = null,
        List<Endereco>? enderecos = null, List<Telefone>? telefones = null, List<Email>? emails = null)
    {
        if (razaoSocial is not null) RazaoSocial = razaoSocial;
        if (nomeFantasia is not null) NomeFantasia = nomeFantasia;
        if (cnpj is not null) Cnpj = cnpj;
        if (enderecos is not null) Enderecos = enderecos;
        if (telefones is not null) Telefones = telefones;
        if (emails is not null) Emails = emails;

        Validar();
    }
    
    private class PessoaJuridicaValidator : AbstractValidator<PessoaJuridica>
    {
        public PessoaJuridicaValidator()
        {
            RuleFor(x => x.InscricaoEstadual)
                .MaximumLength(50);

            RuleFor(x => x.InscricaoMunicipal)
                .MaximumLength(50);

            RuleFor(x => x.Enderecos)
                .NotEmpty();
        }
    }
}