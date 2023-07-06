using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.v1.Unidade;

public class UnidadeFiltroRequest : FiltroRequest
{
    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Descricao { get; set; }

    [MaxLength(10, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(CaseSensitive = false)]
    public string? Sigla { get; set; }
}