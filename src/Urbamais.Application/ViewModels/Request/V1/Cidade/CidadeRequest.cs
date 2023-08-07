using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.V1.Cidade;

public class CidadeRequest : RequestBase
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 2)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(2, ErrorMessage = DataAnnotationsMessages.STRINGLENGHTFIX, MinimumLength = 2)]
    public string? Uf { get; set; }
}