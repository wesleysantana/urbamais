using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Application.ViewModels.Request.V1.Companie;

public class CompanieFilterRequest : FilterRequest
{
    [MaxLength(20, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public ICollection<Telefone>? Phones { get; set; }
    
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public ICollection<Email>? Emails { get; set; }

    public ICollection<Endereco>? Addresses { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? TradeName { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? CorporateName { get; set; }

    [MaxLength(14, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Cnpj { get; set; }

    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? StateRegistration { get; set; }

    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? MunicipalRegistration { get; set; }
}