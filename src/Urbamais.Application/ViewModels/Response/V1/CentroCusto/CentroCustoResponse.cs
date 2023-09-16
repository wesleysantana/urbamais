using Core.ValueObjects;
using Urbamais.Domain.Constants;

namespace Urbamais.Application.ViewModels.Response.V1.CentroCusto;

public class CentroCustoResponse
{
    public int Reduzido { get; set; }

    public Descricao? Descricao { get; set; }

    public Natureza Natureza { get; set; }

    public long Extenso { get; set; }
}