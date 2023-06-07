using Core.Domain.Interfaces;
using Core.SeedWork;

namespace Urbamais.Domain.Entities.Planejamento;

public class Planejamento : BaseEntity, IAggregateRoot
{
    public List<Insumo> _listInsumos = new();   

    public int ObraId { get; private set; }
    public virtual Obra.Obra? Obra { get; private set; }
    public virtual ICollection<PlanejamentoInsumo>? PlanejamentosInsumos { get; private set; }    

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Planejamento()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Planejamento(int obraId)
    {
        ObraId = obraId;
    }

    public override bool Equals(object? obj)
    {
        return obj is Planejamento planejamento &&
            Id == planejamento.Id &&
            ObraId == planejamento.ObraId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ObraId);
    }
}