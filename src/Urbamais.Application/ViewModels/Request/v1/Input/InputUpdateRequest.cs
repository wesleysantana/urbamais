using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Application.ViewModels.Request.V1.Input;

public class InputUpdateRequest : DomainUpdate
{
    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 3)]
    public string? Name { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Description { get; set; }

    public int? UnitId { get; set; }

    [Range(0, 1, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public InputType? Type { get; set; }
}