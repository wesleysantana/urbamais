using Core.Domain;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Application.Interfaces.Core;

public interface ICidadeAppService //: IAppServiceBase<Cidade>
{
    Task<Cidade?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Cidade>> BuscarPorUfAsync(Uf uf, CancellationToken ct = default);
    Task<IReadOnlyList<Cidade>> BuscarPorNomeAsync(string nome, int take = 50, CancellationToken ct = default);
    Task<IReadOnlyList<Cidade>> BuscarPorNomeEUfAsync(string nome, Uf uf, int take = 50, CancellationToken ct = default);
}