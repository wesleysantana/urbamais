using Core.Domain;
using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using System.Reflection;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Colaborador : BaseEntity, IAggregateRoot
{
    private List<Telefone>? _listTelefones = new();
    private List<Email>? _listEmails = new();

    public NomeVO Nome { get; private set; }
    public CpfVO Cpf { get; private set; }
    public Endereco Endereco { get; private set; }
    public string NumeroCarteiraTrabalho { get; private set; }
    public string NumeroCNH { get; private set; }
    public string TipoCNH { get; private set; }
    public DateTime DataValidadeCNH { get; private set; }
    public FileStream CNH { get; private set; }
    public FileStream FichaEPI { get; private set; }
    public FileStream CarteiraTrabalho { get; private set; }
    public string NumeroExameAdmissional { get; private set; }
    public DateTime ValidadeExameAdmissional { get; private set; }
    public FileStream ExameAdmissional { get; private set; }
    public FileStream FichaRegistro { get; private set; }
    public FileStream OrdemServico { get; private set; }

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

    public Colaborador(NomeVO nome, CpfVO cpf,
        Endereco endereco, string numeroCarteiraTrabalho, string numeroCNH, string tipoCNH,
        DateTime dataValidadeCNH, FileStream cNH, FileStream fichaEPI, FileStream carteiraTrabalho,
        string numeroExameAdmissional, DateTime validadeExameAdmissional, FileStream exameAdmissional,
        FileStream fichaRegistro, FileStream ordemServico, List<Telefone>? listTelefones, List<Email>? listEmails)
    {
        Nome = nome;
        Cpf = cpf;
        Endereco = endereco;
        NumeroCarteiraTrabalho = numeroCarteiraTrabalho;
        NumeroCNH = numeroCNH;
        TipoCNH = tipoCNH;
        DataValidadeCNH = dataValidadeCNH;
        CNH = cNH;
        FichaEPI = fichaEPI;
        CarteiraTrabalho = carteiraTrabalho;
        NumeroExameAdmissional = numeroExameAdmissional;
        ValidadeExameAdmissional = validadeExameAdmissional;
        ExameAdmissional = exameAdmissional;
        FichaRegistro = fichaRegistro;
        OrdemServico = ordemServico;
        _listTelefones = listTelefones;
        _listEmails = listEmails;

        Validar();
    }

    private void Validar()
    {
        ValidationResult.Errors.AddRange(Nome.ValidationResult.Errors);
        ValidationResult.Errors.AddRange(Cpf.ValidationResult.Errors);
        ValidationResult.Errors.AddRange(Endereco.ValidationResult.Errors);

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    public void Update(NomeVO? nome = null, CpfVO? cpf = null,
       Endereco? endereco = null, string? numeroCarteiraTrabalho = null, string? numeroCNH = null, string? tipoCNH = null,
       DateTime? dataValidadeCNH = null, FileStream? cNH = null, FileStream? fichaEPI = null, FileStream? carteiraTrabalho = null,
       string? numeroExameAdmissional = null, DateTime? validadeExameAdmissional = null, FileStream? exameAdmissional = null,
       FileStream? fichaRegistro = null, FileStream? ordemServico = null, List<Telefone>? listTelefones = null, List<Email>? listEmails = null)
    {
        if (nome is not null) Nome = nome;
        if (cpf is not null) Cpf = cpf;
        if (endereco is not null) Endereco = endereco;
        if (numeroCarteiraTrabalho is not null) NumeroCarteiraTrabalho = numeroCarteiraTrabalho;
        if (numeroCNH is not null) NumeroCNH = numeroCNH;
        if (tipoCNH is not null) TipoCNH = tipoCNH;
        if (dataValidadeCNH is not null) DataValidadeCNH = (DateTime)dataValidadeCNH;
        if (cNH is not null) CNH = cNH;
        if (fichaEPI is not null) FichaEPI = fichaEPI;
        if (carteiraTrabalho is not null) CarteiraTrabalho = carteiraTrabalho;
        if (numeroExameAdmissional is not null) NumeroExameAdmissional = numeroExameAdmissional;
        if (validadeExameAdmissional is not null) ValidadeExameAdmissional = (DateTime)validadeExameAdmissional;
        if (exameAdmissional is not null) ExameAdmissional = exameAdmissional;
        if (fichaRegistro is not null) FichaRegistro = fichaRegistro;
        if (ordemServico is not null) OrdemServico = ordemServico;
        if (listTelefones is not null) _listTelefones = listTelefones;
        if (listEmails is not null) _listEmails = listEmails;

        Validar();
    }

    #region Sobrescrita Object

    public override string ToString()
    {
        return $"Colaborador - Id: {Id}, Nome: {Nome}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Colaborador colaborador &&
            EqualityComparer<List<Telefone>?>.Default.Equals(_listTelefones, colaborador._listTelefones) &&
            EqualityComparer<List<Email>?>.Default.Equals(_listEmails, colaborador._listEmails) &&
            EqualityComparer<NomeVO>.Default.Equals(Nome, colaborador.Nome) &&
            EqualityComparer<CpfVO>.Default.Equals(Cpf, colaborador.Cpf) &&
            EqualityComparer<Endereco>.Default.Equals(Endereco, colaborador.Endereco) &&
            NumeroCarteiraTrabalho == colaborador.NumeroCarteiraTrabalho &&
            NumeroCNH == colaborador.NumeroCNH &&
            TipoCNH == colaborador.TipoCNH &&
            DataValidadeCNH == colaborador.DataValidadeCNH &&
            EqualityComparer<FileStream>.Default.Equals(CNH, colaborador.CNH) &&
            EqualityComparer<FileStream>.Default.Equals(FichaEPI, colaborador.FichaEPI) &&
            EqualityComparer<FileStream>.Default.Equals(CarteiraTrabalho, colaborador.CarteiraTrabalho) &&
            NumeroExameAdmissional == colaborador.NumeroExameAdmissional &&
            ValidadeExameAdmissional == colaborador.ValidadeExameAdmissional &&
            EqualityComparer<FileStream>.Default.Equals(ExameAdmissional, colaborador.ExameAdmissional) &&
            EqualityComparer<FileStream>.Default.Equals(FichaRegistro, colaborador.FichaRegistro) &&
            EqualityComparer<FileStream>.Default.Equals(OrdemServico, colaborador.OrdemServico);
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(_listTelefones);
        hash.Add(_listEmails);
        hash.Add(Nome);
        hash.Add(Cpf);
        hash.Add(Endereco);
        hash.Add(NumeroCarteiraTrabalho);
        hash.Add(NumeroCNH);
        hash.Add(TipoCNH);
        hash.Add(DataValidadeCNH);
        hash.Add(CNH);
        hash.Add(FichaEPI);
        hash.Add(CarteiraTrabalho);
        hash.Add(NumeroExameAdmissional);
        hash.Add(ValidadeExameAdmissional);
        hash.Add(ExameAdmissional);
        hash.Add(FichaRegistro);
        hash.Add(OrdemServico);
        return hash.ToHashCode();
    }

    #endregion Sobrescrita Object
}