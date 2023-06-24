using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.Unidade;

public class UnidadeRequest
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Descricao { get; set; }
}