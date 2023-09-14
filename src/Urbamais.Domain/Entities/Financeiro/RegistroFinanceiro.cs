using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.Obras;

namespace Urbamais.Domain.Entities.Financeiro;

public class RegistroFinanceiro : BaseEntity, IAggregateRoot
{
    public int IdObra { get; private set; }
    public virtual Obra? Obra { get; private set; }

    // Ainda sem vinculo com o fornecedor registrado na base de dados
    public int IdFornecedor { get; private set; }

    public string Fornecedor { get; private set; }

    public DateTime DataEmissao { get; private set; }
    public DateTime DataVencimento { get; private set; }
    public DateTime DataEntrada { get; private set; }
    public string TipoDoc { get; private set; }
    public string NumeroDoc { get; private set; }
    public int Parcela { get; private set; }
    public int AprovacaoPagamento { get; private set; }
    public ValorMonetario Valor { get; private set; }
    public ValorMonetario Caucao { get; private set; }
    public ValorMonetario Total { get; private set; }
    public ValorMonetario Desconto { get; private set; }
    public ValorMonetario Acrescimo { get; private set; }
    public ValorMonetario ValorLiquido { get; private set; }
    public ValorMonetario ValorBaixa { get; private set; }
    public string Complemento { get; private set; }
    public CentroCusto CentroCusto { get; private set; }
    public CentroCusto ClasseFinanceira { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected RegistroFinanceiro()
    {
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public RegistroFinanceiro(string idUserCreation, int idObra, int idFornecedor, string fornecedor, DateTime dataEmissao,
        DateTime dataVencimento, DateTime dataEntrada, string tipoDoc, string numeroDoc, int parcela, int aprovacaoPagamento,
        ValorMonetario valor, ValorMonetario caucao, ValorMonetario total, ValorMonetario desconto, ValorMonetario acrescimo,
        ValorMonetario valorLiquido, ValorMonetario valorBaixa, string complemento, CentroCusto centroCusto, CentroCusto classeFinanceira)
    {
        IdObra = idObra;
        IdFornecedor = idFornecedor;
        Fornecedor = fornecedor;
        DataEmissao = dataEmissao;
        DataVencimento = dataVencimento;
        DataEntrada = dataEntrada;
        TipoDoc = tipoDoc;
        NumeroDoc = numeroDoc;
        Parcela = parcela;
        AprovacaoPagamento = aprovacaoPagamento;
        Valor = valor;
        Caucao = caucao;
        Total = total;
        Desconto = desconto;
        Acrescimo = acrescimo;
        ValorLiquido = valorLiquido;
        ValorBaixa = valorBaixa;
        Complemento = complemento;
        CentroCusto = centroCusto;
        ClasseFinanceira = classeFinanceira;

        Validate();

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
        else
            IdUserCreation = idUserCreation;
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Valor.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Caucao.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Total.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Desconto.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Acrescimo.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(ValorLiquido.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(ValorBaixa.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(CentroCusto.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(ClasseFinanceira.ValidationResult!.Errors);

        _ = Validate(this, new RegistroFinanceiroValidator());
    }

    public void Update(string idUserModification, int? idObra = null, int? idFornecedor = null, string? fornecedor = null,
        DateTime? dataEmissao = null, DateTime? dataVencimento = null, DateTime? dataEntrada = null, string? tipoDoc = null,
        string? numeroDoc = null, int? parcela = null, int? aprovacaoPagamento = null, ValorMonetario? valor = null,
        ValorMonetario? caucao = null, ValorMonetario? total = null, ValorMonetario? desconto = null, ValorMonetario? acrescimo = null,
        ValorMonetario? valorLiquido = null, ValorMonetario? valorBaixa = null, string? complemento = null,
        CentroCusto? centroCusto = null, CentroCusto? classeFinanceira = null)
    {
        var memento = CreateMemento();

        if (idObra is not null) IdObra = (int)idObra;
        if (idFornecedor is not null) IdFornecedor = (int)idFornecedor;
        if (string.IsNullOrWhiteSpace(fornecedor)) Fornecedor = fornecedor!;
        if (dataEmissao is not null) DataEmissao = (DateTime)dataEmissao;
        if (dataVencimento is not null) DataVencimento = (DateTime)dataVencimento;
        if (dataEntrada is not null) DataEntrada = (DateTime)dataEntrada;
        if (string.IsNullOrWhiteSpace(tipoDoc)) TipoDoc = tipoDoc!;
        if (string.IsNullOrWhiteSpace(numeroDoc)) NumeroDoc = numeroDoc!;
        if (parcela is not null) Parcela = (int)parcela;
        if (aprovacaoPagamento is not null) AprovacaoPagamento = (int)aprovacaoPagamento;
        if (valor is not null) Valor = valor;
        if (caucao is not null) Caucao = caucao;
        if (total is not null) Total = total;
        if (desconto is not null) Desconto = desconto;
        if (acrescimo is not null) Acrescimo = acrescimo;
        if (valorLiquido is not null) ValorLiquido = valorLiquido;
        if (valorBaixa is not null) ValorBaixa = valorBaixa;
        if (string.IsNullOrWhiteSpace(complemento)) Complemento = complemento!;
        if (centroCusto is not null) CentroCusto = centroCusto;
        if (classeFinanceira is not null) ClasseFinanceira = classeFinanceira;

        Validate();

        if (IsValid)
        {
            IdUserModification = idUserModification;
            ModificationDate = DateTime.Now;
        }
        else
            RestoreMemento(memento);
    }

    #region Memento

    private object CreateMemento()
    {
        return new
        {
            IdObra,
            IdFornecedor,
            Fornecedor,
            DataEmissao,
            DataVencimento,
            DataEntrada,
            TipoDoc,
            NumeroDoc,
            Parcela,
            AprovacaoPagamento,
            Valor,
            Caucao,
            Total,
            Desconto,
            Acrescimo,
            ValorLiquido,
            ValorBaixa,
            Complemento,
            CentroCusto,
            ClasseFinanceira
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        IdObra = state.IdObra;
        IdFornecedor = state.IdFornecedor;
        Fornecedor = state.Fornecedor;
        DataEmissao = state.DataEmissao;
        DataVencimento = state.DataVencimento;
        DataEntrada = state.DataEntrada;
        TipoDoc = state.TipoDoc;
        NumeroDoc = state.NumeroDoc;
        Parcela = state.Parcela;
        AprovacaoPagamento = state.AprovacaoPagamento;
        Valor = state.Valor;
        Caucao = state.Caucao;
        Total = state.Total;
        Desconto = state.Desconto;
        Acrescimo = state.Acrescimo;
        ValorLiquido = state.ValorLiquido;
        ValorBaixa = state.ValorBaixa;
        Complemento = state.Complemento;
        CentroCusto = state.CentroCusto;
        ClasseFinanceira = state.ClasseFinanceira;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() =>
        $"Registro Financeiro - Id: {Id}, Id Obra: {IdObra}, Id Fornecedor: {IdFornecedor}, Fornecedor: {Fornecedor}, " +
        $"Data de emissão: {DataEmissao}, Data de vencimento: {DataVencimento}, Data de entrada: {DataEntrada}, Tipo de documento: {TipoDoc}, " +
        $"Número do documento: {NumeroDoc}, Parcela: {Parcela}, Aprovação de pagamento: {AprovacaoPagamento}, Valor: {Valor}, Caução: {Caucao} " +
        $"Total: {Total}, Desconto: {Desconto}, Acréscimo: {Acrescimo}, Valor líquido: {ValorLiquido}, Valor da baixa: {ValorBaixa}, " +
        $"Complemento: {Complemento}, Centro de custo: {CentroCusto}, Classe financeira: {ClasseFinanceira}";

    public override bool Equals(object? obj)
    {
        return obj is RegistroFinanceiro financeiro &&
            Id == financeiro.Id &&
            IdObra == financeiro.IdObra &&
            IdFornecedor == financeiro.IdFornecedor &&
            Fornecedor == financeiro.Fornecedor &&
            DataEmissao == financeiro.DataEmissao &&
            DataVencimento == financeiro.DataVencimento &&
            DataEntrada == financeiro.DataEntrada &&
            TipoDoc == financeiro.TipoDoc &&
            NumeroDoc == financeiro.NumeroDoc &&
            Parcela == financeiro.Parcela &&
            AprovacaoPagamento == financeiro.AprovacaoPagamento &&
            EqualityComparer<ValorMonetario>.Default.Equals(Valor, financeiro.Valor) &&
            EqualityComparer<ValorMonetario>.Default.Equals(Caucao, financeiro.Caucao) &&
            EqualityComparer<ValorMonetario>.Default.Equals(Total, financeiro.Total) &&
            EqualityComparer<ValorMonetario>.Default.Equals(Desconto, financeiro.Desconto) &&
            EqualityComparer<ValorMonetario>.Default.Equals(Acrescimo, financeiro.Acrescimo) &&
            EqualityComparer<ValorMonetario>.Default.Equals(ValorLiquido, financeiro.ValorLiquido) &&
            EqualityComparer<ValorMonetario>.Default.Equals(ValorBaixa, financeiro.ValorBaixa) &&
            Complemento == financeiro.Complemento &&
            EqualityComparer<CentroCusto>.Default.Equals(CentroCusto, financeiro.CentroCusto) &&
            EqualityComparer<CentroCusto>.Default.Equals(ClasseFinanceira, financeiro.ClasseFinanceira);
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(Id);
        hash.Add(IdObra);
        hash.Add(IdFornecedor);
        hash.Add(Fornecedor);
        hash.Add(DataEmissao);
        hash.Add(DataVencimento);
        hash.Add(DataEntrada);
        hash.Add(TipoDoc);
        hash.Add(NumeroDoc);
        hash.Add(Parcela);
        hash.Add(AprovacaoPagamento);
        hash.Add(Valor);
        hash.Add(Caucao);
        hash.Add(Total);
        hash.Add(Desconto);
        hash.Add(Acrescimo);
        hash.Add(ValorLiquido);
        hash.Add(ValorBaixa);
        hash.Add(Complemento);
        hash.Add(CentroCusto);
        hash.Add(ClasseFinanceira);
        return hash.ToHashCode();
    }

    #endregion Sobrescrita Object

    private class RegistroFinanceiroValidator : AbstractValidator<RegistroFinanceiro>
    {
        public RegistroFinanceiroValidator()
        {
            RuleFor(x => x.IdObra)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.IdFornecedor)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Fornecedor)
                .NotEmpty();
        }
    }
}