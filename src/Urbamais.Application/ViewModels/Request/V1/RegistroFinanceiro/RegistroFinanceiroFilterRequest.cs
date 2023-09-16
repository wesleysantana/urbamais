using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.V1.RegistroFinanceiro;

public class RegistroFinanceiroFilterRequest : FilterRequest
{   
    public int? ObraId { get; set; }
   
    public int? FornecedorId { get; set; }
    
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Fornecedor { get; set; }
   
    public DateTime? DataEmissao { get; set; }
    
    public DateTime? DataVencimento { get; set; }
    
    public DateTime? DataEntrada { get; set; }
   
    [MaxLength(25, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? TipoDoc { get; set; }
   
    [MaxLength(25, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? NumeroDoc { get; set; }
    
    public int? Parcela { get; set; }
    
    public int? AprovacaoPagamento { get; set; }
   
    [Range(0.01, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? Valor { get; set; }

    [Range(0.00, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? Caucao { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? Total { get; set; }

    [Range(0.00, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? Desconto { get; set; }

    [Range(0.00, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? Acrescimo { get; set; }
   
    [Range(0.01, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? ValorLiquido { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? ValorBaixa { get; set; }

    [MaxLength(1024, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Complemento { get; set; }
   
    public int? CentroCustoId { get; set; }
    
    public int? ClasseFinanceiraId { get; set; }
}