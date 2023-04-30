using System.Reflection;
using Urbamais.Domain.Entities.Core;
using Urbamais.Domain.ValueObjects;

namespace Urbamais.Domain.Entities.Obra;

public class Empresa : BaseEntity, IAggregateRoot
{
    private List<Telefone>? _listTelefones = new();
    private List<Email>? _listEmails = new();

    public NomeVO RazaoSocial { get; private set; }
    public NomeVO NomeFantasia { get; private set; }
    public CnpjVO Cnpj { get; private set; }
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

    public Empresa(NomeVO razaoSocial, NomeVO nomeFantasia, CnpjVO cnpj, Endereco endereco,
        List<Telefone> telefones, List<Email> emails)
    {
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
        Cnpj = cnpj;
        Endereco = endereco;
        _listTelefones = telefones;
        _listEmails = emails;

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
        $"Empresa - Id: {Id}, Nome: {NomeFantasia}, Razão Sociaol: {RazaoSocial}, Cnpj: {Cnpj}";

    public override bool Equals(object? obj)
    {
        return obj is Empresa empresa &&
            EqualityComparer<NomeVO>.Default.Equals(RazaoSocial, empresa.RazaoSocial) &&
            EqualityComparer<NomeVO>.Default.Equals(NomeFantasia, empresa.NomeFantasia) &&
            EqualityComparer<CnpjVO>.Default.Equals(Cnpj, empresa.Cnpj) &&
            EqualityComparer<Endereco>.Default.Equals(Endereco, empresa.Endereco) &&
            Enumerable.SequenceEqual(_listTelefones!.OrderBy(e => e.Id), empresa._listTelefones!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmails!.OrderBy(e => e.Id), empresa._listEmails!.OrderBy(e => e.Id));
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

    public static bool operator ==(Empresa left, Empresa right) => left.Equals(right);

    public static bool operator !=(Empresa left, Empresa right) => !left.Equals(right);

    #endregion Sobrescrita Object
}