using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.Unidade;

public class UnidadeUpdateRequest : IDomainUpdate
{
    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Descricao { get; set; }

    [MaxLength(10, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Sigla { get; set; }
}