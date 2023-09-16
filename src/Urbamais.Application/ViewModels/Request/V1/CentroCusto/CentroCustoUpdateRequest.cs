using Core.Constants;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Constants;

namespace Urbamais.Application.ViewModels.Request.V1.CentroCusto;

public class CentroCustoUpdateRequest : DomainUpdate
{
    public int? Reduzido { get; set; }

    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public Descricao? Descricao { get; set; }

    public Natureza? Natureza { get; set; }

    public long? Extenso { get; set; }
}