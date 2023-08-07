using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Domain.Entities.Suprimentos;

public class Pedido : BaseEntity, IAggregateRoot
{
    public int PlanejamentoId { get; private set; }
    public virtual Planejamento? Planejamento { get; set; }

    protected Pedido()
    {
    }

    public Pedido(string idUserCreation, int planningId)
    {
        PlanejamentoId = planningId;

        Validate(this, new PedidoValidator());

        if (IsValid)        
            IdUserCreation = idUserCreation;        
    }

    public override string ToString()
    {
        return $"Planejamento - Id: {PlanejamentoId}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Pedido pedido &&
            PlanejamentoId == pedido.PlanejamentoId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PlanejamentoId);
    }

    private class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(x => x.PlanejamentoId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}