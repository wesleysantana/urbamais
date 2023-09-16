using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Constants;

namespace Urbamais.Application.ViewModels.Request.V1.CentroCusto;

public class CentroCustoFilterRequest : FilterRequest
{
    public int? Reduzido { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public Descricao? Descricao { get; set; }

    public Natureza? Natureza { get; set; }

    public long? Extenso { get; set; }
}