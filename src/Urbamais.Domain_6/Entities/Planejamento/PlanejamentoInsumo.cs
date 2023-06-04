using Core.Domain.Interfaces;

namespace Urbamais.Domain.Entities.Planejamento;

public class PlanejamentoInsumo : IEntity
{
    public int PlanejamentoId { get; private set; }
    public virtual Planejamento Planejamento { get; private set; }
    public int InsumoId { get; private set; }
    public virtual Insumo Insumo { get; private set; }
    public int UnidadeId { get; private set; }
    public virtual Unidade Unidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public double Quantidade { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }
}