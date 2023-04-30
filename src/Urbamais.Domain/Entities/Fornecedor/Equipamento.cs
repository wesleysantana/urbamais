using Urbamais.Domain.ValueObjects;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Equipamento : BaseEntity, IAggregateRoot
{
    public DescricaoVO Descricao { get; private set; }
}