using Core.ValueObjects;
using Urbamais.Application.ViewModels.Response.V1.CentroCusto;
using Urbamais.Application.ViewModels.Response.V1.Obra;

namespace Urbamais.Application.ViewModels.Response.V1.RegistroFinanceiro;

public class RegistroFinanceiroResponse : ValidateViewModel
{
    public ObraResponse? Obra { get; set; }
    public int FornecedorId { get; set; }
    public string? Fornecedor { get; set; }
    public DateTime DataEmissao { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime DataEntrada { get; set; }
    public string? TipoDoc { get; set; }
    public string? NumeroDoc { get; set; }
    public int Parcela { get; set; }
    public int AprovacaoPagamento { get; set; }
    public ValorMonetario? Valor { get; set; }
    public ValorMonetario? Caucao { get; set; }
    public ValorMonetario? Total { get; set; }
    public ValorMonetario? Desconto { get; set; }
    public ValorMonetario? Acrescimo { get; set; }
    public ValorMonetario? ValorLiquido { get; set; }
    public ValorMonetario? ValorBaixa { get; set; }
    public string? Complemento { get; set; }
    public CentroCustoResponse? CentroCusto { get; set; }
    public CentroCustoResponse? ClasseFinanceira { get; set; }
}