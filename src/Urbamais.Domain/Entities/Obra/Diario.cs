using Urbamais.Domain.Entities.Fornecedor;

namespace Urbamais.Domain.Entities.Obra;

public class Diario : BaseEntity, IAggregateRoot
{
    public int ObraId { get; private set; }
    public Obra Obra { get; private set; }
    public DateTime Data { get; private set; }
    public int FornecedorId { get; private set; }
    public Fornecedor.Fornecedor Fornecedor { get; private set; }
    public string DescricaoAtividades { get; private set; }
    public int ColaboradorId { get; private set; }
    public Colaborador Colaborador { get; private set; }
    public List<FileStream> Fotos { get; private set; }
}