using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Colaborador : BaseEntity, IAggregateRoot
{
    private List<Telefone>? _listTelefones = new();
    private List<Email>? _listEmails = new();
    private List<Endereco> _listEnderecos = new();

    public NomeVO Nome { get; private set; }
    public CpfVO Cpf { get; private set; }

    public string? NumeroCarteiraTrabalho { get; private set; }
    public string? NumeroCNH { get; private set; }
    public string? TipoCNH { get; private set; }
    public DateTime? DataValidadeCNH { get; private set; }

    public string CNH { get; private set; }
    public string FichaEPI { get; private set; }
    public string CarteiraTrabalho { get; private set; }
    public string? NumeroExameAdmissional { get; private set; }

    public DateTime? DataValidadeExameAdmissional { get; private set; }
    public string ExameAdmissional { get; private set; }
    public string FichaRegistro { get; private set; }
    public string OrdemServico { get; private set; }

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

    protected Colaborador()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Colaborador(NomeVO nome, CpfVO cpf,
        List<Endereco> enderecos, string numeroCarteiraTrabalho, string numeroCNH, string tipoCNH,
        DateTime dataValidadeCNH, string cNH, string fichaEPI, string carteiraTrabalho,
        string numeroExameAdmissional, DateTime validadeExameAdmissional, string exameAdmissional,
        string fichaRegistro, string ordemServico, List<Telefone>? listTelefones, List<Email>? listEmails)
    {
        Nome = nome;
        Cpf = cpf;
        Enderecos = enderecos;
        NumeroCarteiraTrabalho = numeroCarteiraTrabalho;
        NumeroCNH = numeroCNH;
        TipoCNH = tipoCNH;
        DataValidadeCNH = dataValidadeCNH;
        CNH = cNH;
        FichaEPI = fichaEPI;
        CarteiraTrabalho = carteiraTrabalho;
        NumeroExameAdmissional = numeroExameAdmissional;
        DataValidadeExameAdmissional = validadeExameAdmissional;
        ExameAdmissional = exameAdmissional;
        FichaRegistro = fichaRegistro;
        OrdemServico = ordemServico;
        _listTelefones = listTelefones;
        _listEmails = listEmails;

        Validar();
    }

    private void Validar()
    {
        /*
         ValidationResult?.Errors.AddRange(Nome.ValidationResult!.Errors);
         ValidationResult?.Errors.AddRange(Cpf.ValidationResult!.Errors);
         ValidationResult?.Errors.AddRange(Enderecos.SelectMany(x => x.ValidationResult!.Errors));
         ValidationResult?.Errors.AddRange(Telefones.SelectMany(x => x.ValidationResult!.Errors));
         ValidationResult?.Errors.AddRange(Emails.SelectMany(x => x.ValidationResult!.Errors));

         if (!IsValid && Id == default)
         {
             var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
             foreach (var item in propriedades)
             {
                 if (item.Name.Equals(nameof(Enderecos)))
                 {
                     _listTelefones = default;
                     continue;
                 }

                 if (item.Name.Equals(nameof(Telefones)))
                 {
                     _listTelefones = default;
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
        */

        Validate(this, new ColaboradorValidator());
        AddErrorsFrom(Nome);
        AddErrorsFrom(Cpf);
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

    public void Update(NomeVO? nome = null, CpfVO? cpf = null,
       List<Endereco>? enderecos = null, string? numeroCarteiraTrabalho = null, string? numeroCNH = null, string? tipoCNH = null,
       DateTime? dataValidadeCNH = null, string? cNH = null, string? fichaEPI = null, string? carteiraTrabalho = null,
       string? numeroExameAdmissional = null, DateTime? validadeExameAdmissional = null, string? exameAdmissional = null,
       string? fichaRegistro = null, string? ordemServico = null, List<Telefone>? listTelefones = null, List<Email>? listEmails = null)
    {
        if (nome is not null) Nome = nome;
        if (cpf is not null) Cpf = cpf;
        if (enderecos is not null) Enderecos = enderecos;
        if (numeroCarteiraTrabalho is not null) NumeroCarteiraTrabalho = numeroCarteiraTrabalho;
        if (numeroCNH is not null) NumeroCNH = numeroCNH;
        if (tipoCNH is not null) TipoCNH = tipoCNH;
        if (dataValidadeCNH is not null) DataValidadeCNH = (DateTime)dataValidadeCNH;
        if (cNH is not null) CNH = cNH;
        if (fichaEPI is not null) FichaEPI = fichaEPI;
        if (carteiraTrabalho is not null) CarteiraTrabalho = carteiraTrabalho;
        if (numeroExameAdmissional is not null) NumeroExameAdmissional = numeroExameAdmissional;
        if (validadeExameAdmissional is not null) DataValidadeExameAdmissional = (DateTime)validadeExameAdmissional;
        if (exameAdmissional is not null) ExameAdmissional = exameAdmissional;
        if (fichaRegistro is not null) FichaRegistro = fichaRegistro;
        if (ordemServico is not null) OrdemServico = ordemServico;
        if (listTelefones is not null) _listTelefones = listTelefones;
        if (listEmails is not null) _listEmails = listEmails;

        Validar();
    }

    private class ColaboradorValidator : AbstractValidator<Colaborador>
    {
        public ColaboradorValidator()
        {
            RuleFor(x => x.NumeroCarteiraTrabalho)
                .MaximumLength(25);

            RuleFor(x => x.NumeroCNH)
                .Length(9);

            RuleFor(x => x.TipoCNH)
                .MaximumLength(2);

            RuleFor(x => x.NumeroExameAdmissional)
                .MaximumLength(20);
        }
    }
}