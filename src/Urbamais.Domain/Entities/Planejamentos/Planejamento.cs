using Core.Domain.Interfaces;
using Core.SeedWork;
using Urbamais.Domain.Entities.Suprimentos;

namespace Urbamais.Domain.Entities.Planejamentos;

public class Planejamento : BaseEntity, IAggregateRoot
{
    public int ObraId { get; private set; }
    public virtual Obras.Obra? Obra { get; private set; }
    public virtual ICollection<PlanejamentoInsumo>? PlanejamentosInsumos { get; private set; }
    public virtual ICollection<Pedido>? Ordens { get; private set; }   

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Planejamento()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Planejamento(string idUserCreation, int obraId)
    {
        ObraId = obraId;
        IdUserCreation = idUserCreation;
    }

    public void Update(string idUserModification, int obraId)
    {
        ObraId = obraId;
        IdUserModification = idUserModification;
        ModificationDate = DateTime.Now;
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