using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.V1.City;

public class CityUpdateRequest : DomainUpdate
{
    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 2)]
    [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
    public string? Name { get; set; }

    [StringLength(2, ErrorMessage = DataAnnotationsMessages.STRINGLENGHTFIX, MinimumLength = 2)]
    public string? Uf { get; set; }
}