using Urbamais.Application.ViewModels.Response.V1.Unidade;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.ViewModels.Response.V1.Insumo;

public class InsumoResponse : ValidateViewModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public UnidadeResponse? Unidade { get; set; }
    public TipoInsumo Tipo { get; set; }
}