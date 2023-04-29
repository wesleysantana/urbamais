namespace Urbamais.Domain.Entities.Planejamento;

public class Planejamento : BaseEntity, IAggregateRoot
{
    public List<Insumo> _listInsumos = new();   

    public IReadOnlyCollection<Insumo> Insumos
    {
        get => _listInsumos!;
        private set => _listInsumos = value.ToList();
    }

    public int? IdObra { get; private set; }
    public Obra.Obra Obra { get; private set; }

    public Planejamento(IReadOnlyCollection<Insumo> insumos, Obra.Obra obra)
    {
        Insumos = insumos;
        Obra = obra;
    }

    public override bool Equals(object? obj)
    {
        return obj is Planejamento planejamento &&
            Id == planejamento.Id &&
            EqualityComparer<IReadOnlyCollection<Insumo>>.Default.Equals(Insumos, planejamento.Insumos) &&
            IdObra == planejamento.IdObra &&
            EqualityComparer<Obra.Obra>.Default.Equals(Obra, planejamento.Obra);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Insumos, IdObra, Obra);
    }
}