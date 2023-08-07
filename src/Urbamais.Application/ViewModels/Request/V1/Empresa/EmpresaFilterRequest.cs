using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Application.ViewModels.Request.V1.Empresa;

public class EmpresaFilterRequest : FilterRequest
{
    [MaxLength(20, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public ICollection<Telefone>? Telefones { get; set; }
    
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public ICollection<Email>? Emails { get; set; }

    public ICollection<Endereco>? Enderecos { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? NomeFantasia { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? RazaoSocial { get; set; }

    [MaxLength(14, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Cnpj { get; set; }

    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? InscricaoEstadual { get; set; }

    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? InscricaoMunicipal { get; set; }
}