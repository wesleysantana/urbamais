using Core.Constants;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.V1.Obra;

public class ObraRequest : RequestBase
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public required Descricao Descricao { get; set; }
}