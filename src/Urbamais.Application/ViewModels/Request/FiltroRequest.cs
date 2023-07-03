using System.ComponentModel.DataAnnotations.Schema;

namespace Urbamais.Application.ViewModels.Request;

public class FiltroRequest : IFiltroRequest
{
    [NotMapped]
    public int? Limit { get; set; } = 10;

    [NotMapped]
    public int? Offset { get; set; }

    [NotMapped]
    public string? Sort { get; set; }
}