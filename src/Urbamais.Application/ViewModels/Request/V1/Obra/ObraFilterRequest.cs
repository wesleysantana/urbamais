using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.V1.Obra;

public class ObraFilterRequest : FilterRequest
{
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public Descricao? Descricao { get; set; }
}