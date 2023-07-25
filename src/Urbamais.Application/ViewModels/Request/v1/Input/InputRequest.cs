using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Application.ViewModels.Request.v1.Input;

public class InputRequest
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 3)]
    public string? Name { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Description { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int UnitId { get; set; }

    [Required(ErrorMessage =DataAnnotationsMessages.REQUIRED)]
    [Range(0, 1, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public InputType Type { get; set; }
}