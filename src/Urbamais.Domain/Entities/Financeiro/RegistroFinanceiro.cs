using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.Obras;

namespace Urbamais.Domain.Entities.Financeiro;

public class RegistroFinanceiro : BaseEntity, IAggregateRoot
{
    public int ObraId { get; private set; }
    public virtual Obra? Obra { get; private set; }

    // Ainda sem vinculo com o fornecedor registrado na base de dados
    public int FornecedorId { get; private set; }

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
    public int CentroCustoId { get; private set; }
    public virtual CentroCusto? CentroCusto { get; private set; }
    public int ClasseFinanceiraId { get; private set; }
    public virtual CentroCusto? ClasseFinanceira { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected RegistroFinanceiro()
    {
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public RegistroFinanceiro(string idUserCreation, int obraId, int fornecedorId, string fornecedor, DateTime dataEmissao,
        DateTime dataVencimento, DateTime dataEntrada, string tipoDoc, string numeroDoc, int parcela, int aprovacaoPagamento,
        ValorMonetario valor, ValorMonetario caucao, ValorMonetario total, ValorMonetario desconto, ValorMonetario acrescimo,
        ValorMonetario valorLiquido, ValorMonetario valorBaixa, string complemento, int centroCustoId, int classeFinanceiraId)
    {
        ObraId = obraId;
        FornecedorId = fornecedorId;
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
        CentroCustoId = centroCustoId;
        ClasseFinanceiraId = classeFinanceiraId;

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

        _ = Validate(this, new RegistroFinanceiroValidator());
    }

    public void Update(string idUserModification, int? obraId = null, int? fornecedorId = null, string? fornecedor = null,
        DateTime? dataEmissao = null, DateTime? dataVencimento = null, DateTime? dataEntrada = null, string? tipoDoc = null,
        string? numeroDoc = null, int? parcela = null, int? aprovacaoPagamento = null, ValorMonetario? valor = null,
        ValorMonetario? caucao = null, ValorMonetario? total = null, ValorMonetario? desconto = null, ValorMonetario? acrescimo = null,
        ValorMonetario? valorLiquido = null, ValorMonetario? valorBaixa = null, string? complemento = null,
        CentroCusto? centroCusto = null, CentroCusto? classeFinanceira = null)
    {
        var memento = CreateMemento();

        if (obraId is not null) ObraId = (int)obraId;
        if (fornecedorId is not null) FornecedorId = (int)fornecedorId;
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
            ObraId,
            FornecedorId,
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
            CentroCustoId,
            ClasseFinanceiraId
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        ObraId = state.ObraId;
        FornecedorId = state.IdFornecedor;
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
        CentroCustoId = state.CentroCustoId;
        ClasseFinanceiraId = state.ClasseFinanceiraId;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() =>
        $"Registro Financeiro - Id: {Id}, Id Obra: {ObraId}, Id Fornecedor: {FornecedorId}, Fornecedor: {Fornecedor}, " +
        $"Data de emissão: {DataEmissao}, Data de vencimento: {DataVencimento}, Data de entrada: {DataEntrada}, Tipo de documento: {TipoDoc}, " +
        $"Número do documento: {NumeroDoc}, Parcela: {Parcela}, Aprovação de pagamento: {AprovacaoPagamento}, Valor: {Valor}, Caução: {Caucao} " +
        $"Total: {Total}, Desconto: {Desconto}, Acréscimo: {Acrescimo}, Valor líquido: {ValorLiquido}, Valor da baixa: {ValorBaixa}, " +
        $"Complemento: {Complemento}, Id Centro de custo: {CentroCustoId}, Id Classe financeira: {ClasseFinanceiraId}";

    public override bool Equals(object? obj)
    {
        return obj is RegistroFinanceiro financeiro &&
            Id == financeiro.Id &&
            ObraId == financeiro.ObraId &&
            FornecedorId == financeiro.FornecedorId &&
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
            CentroCustoId == financeiro.CentroCustoId &&
            ClasseFinanceiraId == financeiro.ClasseFinanceiraId;
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(Id);
        hash.Add(ObraId);
        hash.Add(FornecedorId);
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
        hash.Add(CentroCustoId);
        hash.Add(ClasseFinanceiraId);
        return hash.ToHashCode();
    }

    #endregion Sobrescrita Object

    private class RegistroFinanceiroValidator : AbstractValidator<RegistroFinanceiro>
    {
        public RegistroFinanceiroValidator()
        {
            RuleFor(x => x.ObraId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.FornecedorId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Fornecedor)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.DataEmissao)
                .Must(obj => obj != default);

            RuleFor(x => x.DataVencimento)
               .Must(obj => obj != default);

            RuleFor(x => x.DataEntrada)
               .Must(obj => obj != default);

            RuleFor(x => x.TipoDoc)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(x => x.NumeroDoc)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(x => x.Parcela)
                .GreaterThan(0);

            RuleFor(x => x.AprovacaoPagamento)
                .GreaterThan(0);

            RuleFor(x => x.Complemento)
                .MaximumLength(1024);

            RuleFor(x => x.CentroCustoId)
                .GreaterThan(0);

            RuleFor(x => x.ClasseFinanceiraId)
                .GreaterThan(0);
        }
    }
}