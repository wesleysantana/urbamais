using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.V1.Unit;

public class UnitFilterRequest : FilterRequest
{
    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Description { get; set; }

    [MaxLength(10, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(CaseSensitive = false)]
    public string? Acronym { get; set; }
}