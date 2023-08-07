using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.ViewModels.Request.V1.Input;

public class InputFilterRequest : FilterRequest
{
    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 3)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Name { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Description { get; set; }

    public int? UnitId { get; set; }

    [Range(0, 1, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public TipoInsumo? Type { get; set; }
}