using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.ViewModels.Request.V1.Insumo;

public class InsumoFilterRequest : FilterRequest
{
    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 3)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Nome { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Descricao { get; set; }

    public int? UnidadeId { get; set; }

    [Range(0, 1, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public TipoInsumo? Tipo { get; set; }
}