using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Urbamais.Domain.Entities.Supply;

public class Order : BaseEntity, IAggregateRoot
{
    public int PlanningId { get; private set; }
    public virtual Planning.Planning? Planning { get; set; }

    protected Order()
    {
    }

    public Order(int planningId)
    {
        PlanningId = planningId;

        Validate(this, new OrderValidator());
    }

    public override string ToString()
    {
        return $"Planejamento - Id: {PlanningId}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Order pedido &&
            PlanningId == pedido.PlanningId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PlanningId);
    }

    private class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.PlanningId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}