using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;

namespace Urbamais.Domain.Entities.Suprimento;

public class Pedido : BaseEntity, IAggregateRoot
{
    public int PlanejamentoId { get; private set; }
    public virtual Planejamento.Planejamento? Planejamento { get; set; }

    protected Pedido()
    {
    }

    public Pedido(int planejamentoId)
    {
        PlanejamentoId = planejamentoId;

        Validate(this, new PedidoValidator());
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