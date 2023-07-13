using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.v1.Unit;

public class UnitRequest
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Description { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(10, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Acronym { get; set; }
}