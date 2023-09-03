using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Domain.Entities.Fornecedores;

public class Colaborador : BaseEntity, IAggregateRoot
{
    private List<Telefone>? _listTelefone = new();
    private List<Email>? _listEmail = new();
    private List<Endereco> _listEndereco = new();

    public Nome Nome { get; private set; }
    public Cpf Cpf { get; private set; }

    public string? NumeroCTPS { get; private set; }
    public string? NumeroCNH { get; private set; }
    public string? TipoCNH { get; private set; }
    public DateTime? DataExpiracaoCNH { get; private set; }

    public string? CNH { get; private set; }
    public string? EPI { get; private set; }
    public string? CTPS { get; private set; }
    public string? NumeroExameAdmissional { get; private set; }

    public DateTime? DataExpiracaoExameAdmissional { get; private set; }
    public string? ExameAdimissional { get; private set; }
    public string? FichaRegistro { get; private set; }
    public string? OrdemServico { get; private set; }

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

    protected Colaborador()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Colaborador(string idUserCreation, Nome name, Cpf cpf,
        List<Endereco> enderecos, string numeroCtps, string numeroCNH, string tipoCnh,
        DateTime dataExpiracaoCNH, string cnh, string epi, string ctps,
        string numeroExameAdmissional, DateTime validadeExameAdmissional, string exameAdimissional,
        string fichaRegistro, string ordemServico, List<Telefone>? listTelefones, List<Email>? listEmails)
    {
        Nome = name;
        Cpf = cpf;
        Enderecos = enderecos;
        NumeroCTPS = numeroCtps;
        NumeroCNH = numeroCNH;
        TipoCNH = tipoCnh;
        DataExpiracaoCNH = dataExpiracaoCNH;
        CNH = cnh;
        EPI = epi;
        CTPS = ctps;
        NumeroExameAdmissional = numeroExameAdmissional;
        DataExpiracaoExameAdmissional = validadeExameAdmissional;
        ExameAdimissional = exameAdimissional;
        FichaRegistro = fichaRegistro;
        OrdemServico = ordemServico;
        _listTelefone = listTelefones;
        _listEmail = listEmails;

        Validate();

        if (IsValid)
            IdUserCreation = idUserCreation;
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Nome.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Cpf.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Enderecos.SelectMany(x => x.ValidationResult!.Errors));
        ValidationResult?.Errors.AddRange(Telefones.SelectMany(x => x.ValidationResult!.Errors));
        ValidationResult?.Errors.AddRange(Emails.SelectMany(x => x.ValidationResult!.Errors));

        if (!IsValid && Id == default)
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in properties)
            {
                if (item.Name.Equals(nameof(Enderecos)))
                {
                    _listTelefone = default;
                    continue;
                }

                if (item.Name.Equals(nameof(Telefones)))
                {
                    _listTelefone = default;
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

    public void Update(string idUserModification, Nome? name = null, Cpf? cpf = null,
       List<Endereco>? enderecos = null, string? numeroCTPS = null, string? numeroCnh = null, string? tipoCnh = null,
       DateTime? dataExpiracaoCnh = null, string? cnh = null, string? epi = null, string? ctps = null,
       string? numeroExameAdmissional = null, DateTime? dataExpiracaoExameAdmissional = null,
       string? exameAdimissional = null, string? fichaRegistro = null, string? ordemServico = null,
       List<Telefone>? listTelefone = null, List<Email>? listEmails = null)
    {
        var memento = CreateMemento();

        if (name is not null) Nome = name;
        if (cpf is not null) Cpf = cpf;
        if (enderecos is not null) Enderecos = enderecos;
        if (numeroCTPS is not null) NumeroCTPS = numeroCTPS;
        if (numeroCnh is not null) NumeroCNH = numeroCnh;
        if (tipoCnh is not null) TipoCNH = tipoCnh;
        if (dataExpiracaoCnh is not null) DataExpiracaoCNH = (DateTime)dataExpiracaoCnh;
        if (cnh is not null) CNH = cnh;
        if (epi is not null) EPI = epi;
        if (ctps is not null) CTPS = ctps;
        if (numeroExameAdmissional is not null) NumeroExameAdmissional = numeroExameAdmissional;
        if (dataExpiracaoExameAdmissional is not null) DataExpiracaoExameAdmissional = (DateTime)dataExpiracaoExameAdmissional;
        if (exameAdimissional is not null) ExameAdimissional = exameAdimissional;
        if (fichaRegistro is not null) FichaRegistro = fichaRegistro;
        if (ordemServico is not null) OrdemServico = ordemServico;
        if (listTelefone is not null) _listTelefone = listTelefone;
        if (listEmails is not null) _listEmail = listEmails;

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
            Nome,
            Cpf,
            Enderecos,
            NumeroCTPS,
            NumeroCNH,
            TipoCNH,
            DataExpiracaoCNH,
            CNH,
            EPI,
            CTPS,
            NumeroExameAdmissional,
            DataExpiracaoExameAdmissional,
            ExameAdimissional,
            FichaRegistro,
            OrdemServico,
            _listTelefone,
            _listEmail
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Nome = state.Nome;
        Cpf = state.Cpf;
        Enderecos = state.Enderecos;
        NumeroCTPS = state.NumeroCTPS;
        NumeroCNH = state.NumeroCNH;
        TipoCNH = state.TipoCNH;
        DataExpiracaoCNH = state.DataExpiracaoCNH;
        CNH = state.CNH;
        EPI = state.EPI;
        CTPS = state.CTPS;
        NumeroExameAdmissional = state.NumeroExameAdmissional;
        DataExpiracaoExameAdmissional = state.DataExpiracaoExameAdmissional;
        ExameAdimissional = state.ExameAdimissional;
        FichaRegistro = state.FichaRegistro;
        OrdemServico = state.OrdemServico;
        _listTelefone = state._listTelefone;
        _listEmail = state._listEmail;
    }

    #endregion memento

    #region Sobrescrita Object

    public override string ToString()
    {
        return $"Colaborador - Id: {Id}, Nome: {Nome}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Colaborador colaborador &&
            EqualityComparer<Nome>.Default.Equals(Nome, colaborador.Nome) &&
            EqualityComparer<Cpf>.Default.Equals(Cpf, colaborador.Cpf) &&
            NumeroCTPS == colaborador.NumeroCTPS &&
            NumeroCNH == colaborador.NumeroCNH &&
            TipoCNH == colaborador.TipoCNH &&
            DataExpiracaoCNH == colaborador.DataExpiracaoCNH &&
            EqualityComparer<string>.Default.Equals(CNH, colaborador.CNH) &&
            EqualityComparer<string>.Default.Equals(EPI, colaborador.EPI) &&
            EqualityComparer<string>.Default.Equals(CTPS, colaborador.CTPS) &&
            NumeroExameAdmissional == colaborador.NumeroExameAdmissional &&
            DataExpiracaoExameAdmissional == colaborador.DataExpiracaoExameAdmissional &&
            EqualityComparer<string>.Default.Equals(ExameAdimissional, colaborador.ExameAdimissional) &&
            EqualityComparer<string>.Default.Equals(FichaRegistro, colaborador.FichaRegistro) &&
            EqualityComparer<string>.Default.Equals(OrdemServico, colaborador.OrdemServico) &&
            Enumerable.SequenceEqual(_listEndereco!.OrderBy(e => e.Id), colaborador._listEndereco!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listTelefone!.OrderBy(e => e.Id), colaborador._listTelefone!.OrderBy(e => e.Id)) &&
            Enumerable.SequenceEqual(_listEmail!.OrderBy(e => e.Id), colaborador._listEmail!.OrderBy(e => e.Id));
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(_listTelefone);
        hash.Add(_listEmail);
        hash.Add(Nome);
        hash.Add(Cpf);
        hash.Add(Enderecos);
        hash.Add(NumeroCTPS);
        hash.Add(NumeroCNH);
        hash.Add(TipoCNH);
        hash.Add(DataExpiracaoCNH);
        hash.Add(CNH);
        hash.Add(EPI);
        hash.Add(CTPS);
        hash.Add(NumeroExameAdmissional);
        hash.Add(DataExpiracaoExameAdmissional);
        hash.Add(ExameAdimissional);
        hash.Add(FichaRegistro);
        hash.Add(OrdemServico);
        return hash.ToHashCode();
    }

    #endregion Sobrescrita Object

    private class ColaboradorValidator : AbstractValidator<Colaborador>
    {
        public ColaboradorValidator()
        {
            RuleFor(x => x.NumeroCTPS)
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