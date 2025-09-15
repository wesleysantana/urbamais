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