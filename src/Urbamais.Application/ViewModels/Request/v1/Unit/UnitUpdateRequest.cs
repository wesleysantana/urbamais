using Core.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Urbamais.Application.ViewModels.Request.v1.Unit;

public class UnitUpdateRequest : IDomainUpdate
{
    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Description { get; set; }

    [MaxLength(10, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Acronym { get; set; }

    [JsonIgnore]
    public string IdUserModification { get; set; } = string.Empty;
}