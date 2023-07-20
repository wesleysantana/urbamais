using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;

namespace Urbamais.Domain.Entities.Planning;

public class Reprogramming : BaseEntity, IAggregateRoot
{
    public int PlanningInputPlanningId { get; private set; }
    public int PlanningInputInputId { get; private set; }
    public virtual PlanningInput? PlanningInput { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime FinalDate { get; private set; }
    public DescriptionVO Description { get; private set; }
    public decimal UnitaryValue { get; private set; }
}