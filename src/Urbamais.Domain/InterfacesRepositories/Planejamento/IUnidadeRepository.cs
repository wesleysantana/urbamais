using Urbamais.Domain.Entities.Planejamento;
//using Urbamais.Domain.InterfacesRepositories.Generic;

namespace Urbamais.Domain.InterfacesRepositories.Planejamento;

public interface IUnidadeRepository //: IRepositoryBase<Unidade>
{

    // Escrita
    Task AddAsync(Unidade entity, CancellationToken ct = default);
    void Update(Unidade entity);
    void Remove(Unidade entity);

    // Leitura (consultas nomeadas)
    Task<Unidade?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Unidade?> ObterPorSiglaAsync(string sigla, CancellationToken ct = default);
    Task<IReadOnlyList<Unidade>> BuscarPorDescricaoAsync(string termo, int take = 50, CancellationToken ct = default);
    Task<bool> ExisteSiglaAsync(string sigla, CancellationToken ct = default);
}