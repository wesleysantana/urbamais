using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.ViewModels.Request.V1.Input;

public class InputRequest : RequestBase
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 3)]
    public string? Name { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Description { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int UnitId { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [Range(0, 1, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public TipoInsumo Type { get; set; }
}