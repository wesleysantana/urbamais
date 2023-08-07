using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Urbamais.Domain.Entities.Supply;

public class Order : BaseEntity, IAggregateRoot
{
    public int PlanningId { get; private set; }
    public virtual Planejamentos.Planejamento? Planning { get; set; }

    protected Order()
    {
    }

    public Order(string idUserCreation, int planningId)
    {
        PlanningId = planningId;

        Validate(this, new OrderValidator());

        if (IsValid)        
            IdUserCreation = idUserCreation;        
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