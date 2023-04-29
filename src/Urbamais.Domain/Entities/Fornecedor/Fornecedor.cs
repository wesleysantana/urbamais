using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.Core;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Domain.ValueObjects;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Fornecedor : BaseEntity, IAggregateRoot
{
    private List<Telefone>? _listTelefones = new();
    private List<Email>? _listEmails = new();

    public NomeVO NomeFantasia { get; private set; }
    public NomeVO RazaoSocial { get; private set; }
    public CnpjVO Cnpj { get; private set; }
    public string InscricaoEstadual { get; private set; }
    public string? InscricaoMunicipal { get; private set; }
    public Endereco Endereco { get; private set; }

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

    public Fornecedor(NomeVO nomeFantasia, NomeVO razaoSocial, CnpjVO cnpj, string inscricaoEstadual, 
        string? inscricaoMunicipal, Endereco endereco, List<Telefone>? listTelefones, List<Email>? listEmails)
    {        
        NomeFantasia = nomeFantasia;
        RazaoSocial = razaoSocial;
        Cnpj = cnpj;
        InscricaoEstadual = inscricaoEstadual;
        InscricaoMunicipal = inscricaoMunicipal;
        Endereco = endereco;
        _listTelefones = listTelefones;
        _listEmails = listEmails;

        Validar();
    }

    private void Validar()
    {
        ValidationResult.Errors.AddRange(RazaoSocial.ValidationResult.Errors);
        ValidationResult.Errors.AddRange(NomeFantasia.ValidationResult.Errors);
        ValidationResult.Errors.AddRange(Cnpj.ValidationResult.Errors);
        ValidationResult.Errors.AddRange(Endereco.ValidationResult.Errors);
        ValidationResult.Errors.AddRange(Telefones.SelectMany(x => x.ValidationResult.Errors));
        ValidationResult.Errors.AddRange(Emails.SelectMany(x => x.ValidationResult.Errors));

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
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
    }

    public void Update(NomeVO? razaoSocial = null, NomeVO? nomeFantasia = null, CnpjVO? cnpj = null, Endereco? endereco = null,
        List<Telefone>? telefones = null, List<Email>? emails = null)
    {
        if (razaoSocial is not null) RazaoSocial = razaoSocial;
        if (nomeFantasia is not null) NomeFantasia = nomeFantasia;
        if (cnpj is not null) Cnpj = cnpj;
        if (endereco is not null) Endereco = endereco;
        if (telefones is not null) Telefones = telefones;
        if (emails is not null) Emails = emails;

        Validar();
    }

    #region Sobrescrita Object

    public override string ToString() =>
        $"Fornecedor - Id: {Id}, Nome: {NomeFantasia}, Razão Sociaol: {RazaoSocial}, Cnpj: {Cnpj}";

    public override bool Equals(object? obj)
    {
        return obj is Fornecedor fornecedor &&
            EqualityComparer<NomeVO>.Default.Equals(RazaoSocial, fornecedor.RazaoSocial) &&
            EqualityComparer<NomeVO>.Default.Equals(NomeFantasia, fornecedor.NomeFantasia) &&
            EqualityComparer<CnpjVO>.Default.Equals(Cnpj, fornecedor.Cnpj) &&
            EqualityComparer<Endereco>.Default.Equals(Endereco, fornecedor.Endereco) &&
            Enumerable.SequenceEqual(_listTelefones!.OrderBy(e => e.Id), fornecedor._listTelefones!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmails!.OrderBy(e => e.Id), fornecedor._listEmails!.OrderBy(e => e.Id));
    }

    public override int GetHashCode()
    {
        int hashTel = 0;
        foreach (var item in _listTelefones!)
        {
            hashTel += item.GetHashCode();
        }

        int hashEmail = 0;
        foreach (var item in _listTelefones!)
        {
            hashEmail += item.GetHashCode();
        }

        return HashCode.Combine(Id, RazaoSocial, NomeFantasia, Cnpj, Endereco) + hashTel + hashEmail;
    }

    public static bool operator ==(Fornecedor left, Fornecedor right) => left.Equals(right);

    public static bool operator !=(Fornecedor left, Fornecedor right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class FornecedorValidator : AbstractValidator<Fornecedor>
    {
        public FornecedorValidator()
        {
            RuleFor(x => x.InscricaoEstadual)
                .MaximumLength(50);

            RuleFor(x => x.InscricaoMunicipal)
                .MaximumLength(50);
        }
    }
}