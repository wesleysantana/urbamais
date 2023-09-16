using Core.Constants;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Constants;

namespace Urbamais.Application.ViewModels.Request.V1.CentroCusto;

public class CentroCustoRequest : RequestBase
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public int Reduzido { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public required Descricao Descricao { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public Natureza Natureza { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public long Extenso { get; set; }
}