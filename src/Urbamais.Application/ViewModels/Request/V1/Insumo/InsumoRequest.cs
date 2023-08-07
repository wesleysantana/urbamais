using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.ViewModels.Request.V1.Insumo;

public class InsumoRequest : RequestBase
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 3)]
    public string? Nome { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int UnidadeId { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [Range(0, 1, ErrorMessage = DataAnnotationsMessages.RANGE)]
    public TipoInsumo Tipo { get; set; }
}