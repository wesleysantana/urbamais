namespace Urbamais.Domain.Entities.Obra;

public class Diario : BaseEntity, IAggregateRoot
{
    public int ObraId { get; private set; }
    public Obra Obra { get; private set; }
    public int FornecedorId { get; private set; }
    public Fornecedor.Fornecedor Fornecedor { get; private set; }
}