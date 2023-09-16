using Core.Constants;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.V1.RegistroFinanceiro;

public class RegistroFinanceiroRequest : RequestBase
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int ObraId { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int FornecedorId { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public required string Fornecedor { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public DateTime DataEmissao { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public DateTime DataVencimento { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public DateTime DataEntrada { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(25, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public required string TipoDoc { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(25, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public required string NumeroDoc { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int Parcela { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int AprovacaoPagamento { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [Range(0.01, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public required ValorMonetario Valor { get; set; }
    
    [Range(0.00, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? Caucao { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [Range(0.01, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public required ValorMonetario Total { get; set; }
   
    [Range(0.00, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? Desconto { get; set; }

    [Range(0.00, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? Acrescimo { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [Range(0.01, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public required ValorMonetario ValorLiquido { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public ValorMonetario? ValorBaixa { get; set; }

    [MaxLength(1024, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Complemento { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int CentroCustoId { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int ClasseFinanceiraId { get; set; }
}