using Urbamais.Application.Shared;
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.Application.Interfaces.Planejamento;

public interface IUnidadeAppService // : IAppServiceBase<Unidade>
{
    // READs (retornam domínio; o controller mapeia para UnidadeResponse)
    Task<Unidade?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<Unidade?> ObterPorSiglaAsync(string sigla, CancellationToken ct = default);
    Task<IReadOnlyList<Unidade>> BuscarPorDescricaoAsync(string termo, int take = 50, CancellationToken ct = default);

    // WRITEs (App decide soft/hard; retornam Result padronizado)
    Task<Result<int>> CadastrarAsync(UnidadeRequest request, CancellationToken ct = default);
    Task<Result> AtualizarAsync(int id, UnidadeUpdateRequest request, CancellationToken ct = default);

    // Soft delete (recomendado): marca DataExclusao e persiste via Update
    Task<Result> ExcluirAsync(int id, CancellationToken ct = default);

    // Hard delete (casos excepcionais)
    Task<Result> ExcluirDefinitivoAsync(int id, CancellationToken ct = default);

    // (Opcional) Restaurar item soft-deletado
    Task<Result> RestaurarAsync(int id, CancellationToken ct = default);
}