using Core.Constants;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.V1.Obra;

public class ObraUpdateRequest : DomainUpdate
{
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public Descricao? Descricao { get; set; }
}