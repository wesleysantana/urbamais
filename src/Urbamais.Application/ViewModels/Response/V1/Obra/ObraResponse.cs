using Core.ValueObjects;

namespace Urbamais.Application.ViewModels.Response.V1.Obra;

public class ObraResponse : ValidateViewModel
{
    public int Id { get; set; }
    public Descricao? Descricao { get; set; }
}