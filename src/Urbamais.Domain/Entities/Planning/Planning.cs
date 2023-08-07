using Core.Domain.Interfaces;
using Core.SeedWork;
using Urbamais.Domain.Entities.Supply;

namespace Urbamais.Domain.Entities.Planning;

public class Planning : BaseEntity, IAggregateRoot
{
    public int ConstructionId { get; private set; }
    public virtual Obras.Obra? Construction { get; private set; }
    public virtual ICollection<PlanningInput>? PlannigInputs { get; private set; }
    public virtual ICollection<Order>? Orders { get; private set; }   

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Planning()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Planning(string idUserCreation, int constructionId)
    {
        ConstructionId = constructionId;
        IdUserCreation = idUserCreation;
    }

    public void Update(string idUserModification, int constructionId)
    {
        ConstructionId = constructionId;
        IdUserModification = idUserModification;
        ModificationDate = DateTime.Now;
    }

    public override bool Equals(object? obj)
    {
        return obj is Planning planejamento &&
            Id == planejamento.Id &&
            ConstructionId == planejamento.ConstructionId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ConstructionId);
    }
}