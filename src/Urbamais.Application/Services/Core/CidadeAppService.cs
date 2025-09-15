using Core.Domain;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.InterfacesRepositories.Core;

namespace Urbamais.Application.Services.Core;

//public class CidadeAppService : AppServiceBase<Cidade>, ICidadeAppService
public class CidadeAppService : ICidadeAppService{
    
    private readonly ICidadeRepository _repo;
    public CidadeAppService(ICidadeRepository repo) => _repo = repo;

    public Task<Cidade?> ObterPorIdAsync(int id, CancellationToken ct = default) => _repo.GetByIdAsync(id, ct);

    public Task<IReadOnlyList<Cidade>> BuscarPorUfAsync(Uf uf, CancellationToken ct = default) => _repo.BuscarPorUfAsync(uf, 200, ct);

    public Task<IReadOnlyList<Cidade>> BuscarPorNomeAsync(string nome, int take = 50, CancellationToken ct = default)
        => _repo.BuscarPorNomeAsync(nome, take, ct);

    public Task<IReadOnlyList<Cidade>> BuscarPorNomeEUfAsync(string nome, Uf uf, int take = 50, CancellationToken ct = default)
        => _repo.BuscarPorNomeEUfAsync(nome, uf, take, ct);    
}