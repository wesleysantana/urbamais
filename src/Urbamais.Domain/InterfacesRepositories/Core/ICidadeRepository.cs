using Core.Domain;
using Urbamais.Domain.Entities.EntitiesOfCore;
// using Urbamais.Domain.InterfacesRepositories.Generic;

namespace Urbamais.Domain.InterfacesRepositories.Core;

public interface ICidadeRepository //: IRepositoryBase<Cidade>
{
    // Escrita
    Task AddAsync(Cidade entity, CancellationToken ct = default);
    void Update(Cidade entity);
    void Remove(Cidade entity);

    // Leitura
    Task<Cidade?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Cidade>> BuscarPorUfAsync(Uf uf, int take = 200, CancellationToken ct = default);
    Task<IReadOnlyList<Cidade>> BuscarPorNomeAsync(string nome, int take = 50, CancellationToken ct = default);
    Task<IReadOnlyList<Cidade>> BuscarPorNomeEUfAsync(string nome, Uf uf, int take = 50, CancellationToken ct = default);
}