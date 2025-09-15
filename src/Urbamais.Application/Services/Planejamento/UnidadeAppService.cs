using Core.Domain.Interfaces; // IUnitOfWork
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Shared; // Result / Result<T>
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Domain.Entities.Planejamento;
using Urbamais.Domain.InterfacesRepositories.Planejamento;

namespace Urbamais.Application.Services.Planejamento
{
    public sealed class UnidadeAppService : IUnidadeAppService
    {
        private readonly IUnidadeRepository _repo;
        private readonly IUnitOfWork _uow;

        public UnidadeAppService(IUnidadeRepository repo, IUnitOfWork uow)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        // READs
        public Task<Unidade?> ObterPorIdAsync(int id, CancellationToken ct = default)
            => _repo.GetByIdAsync(id, ct);

        public Task<Unidade?> ObterPorSiglaAsync(string sigla, CancellationToken ct = default)
            => _repo.ObterPorSiglaAsync(sigla, ct);

        public Task<IReadOnlyList<Unidade>> BuscarPorDescricaoAsync(string termo, int take = 50, CancellationToken ct = default)
            => _repo.BuscarPorDescricaoAsync(termo, take, ct);

        // WRITEs
        public async Task<Result<int>> CadastrarAsync(UnidadeRequest req, CancellationToken ct = default)
        {
            if (req is null) return Result<int>.Fail("Requisição inválida.");
            if (string.IsNullOrWhiteSpace(req.Descricao)) return Result<int>.Fail("Descrição é obrigatória.");
            if (string.IsNullOrWhiteSpace(req.Sigla)) return Result<int>.Fail("Sigla é obrigatória.");

            var sigla = req.Sigla.Trim().ToUpperInvariant();
            if (await _repo.ExisteSiglaAsync(sigla, ct)) return Result<int>.Conflict("Sigla já existente.");

            var unidade = new Unidade(req.Descricao.Trim(), sigla);
            await _repo.AddAsync(unidade, ct);
            await _uow.SaveChangesAsync(ct);

            return Result<int>.Ok(unidade.Id);
        }

        public async Task<Result> AtualizarAsync(int id, UnidadeUpdateRequest req, CancellationToken ct = default)
        {
            var u = await _repo.GetByIdAsync(id, ct);
            if (u is null) return Result.NotFound("Unidade não encontrada.");

            // Unicidade: só checa se veio sigla e se mudou (após normalização)
            if (!string.IsNullOrWhiteSpace(req.Sigla))
            {
                var novaSigla = req.Sigla.Trim().ToUpperInvariant();
                if (!string.Equals(novaSigla, u.Sigla, StringComparison.Ordinal))
                {
                    if (await _repo.ExisteSiglaAsync(novaSigla, ct))
                        return Result.Conflict("Sigla já existente.");
                }
            }
            
            u.Update(req.Descricao, req.Sigla);

            
            if (u is { IsValid: false })
                return Result.Fail("Dados inválidos.");

            _repo.Update(u);
            await _uow.SaveChangesAsync(ct);
            return Result.Ok();
        }


        // SOFT delete
        public async Task<Result> ExcluirAsync(int id, CancellationToken ct = default)
        {
            var u = await _repo.GetByIdAsync(id, ct);
            if (u is null) return Result.NotFound("Unidade não encontrada.");

            u.Delete();
            _repo.Update(u);
            await _uow.SaveChangesAsync(ct);
            return Result.Ok();
        }

        // HARD delete (excepcional)
        public async Task<Result> ExcluirDefinitivoAsync(int id, CancellationToken ct = default)
        {
            var u = await _repo.GetByIdAsync(id, ct);
            if (u is null) return Result.NotFound("Unidade não encontrada.");

            _repo.Remove(u);
            await _uow.SaveChangesAsync(ct);
            return Result.Ok();
        }

        // Restore (soft-delete)
        public async Task<Result> RestaurarAsync(int id, CancellationToken ct = default)
        {
            var u = await _repo.GetByIdAsync(id, ct);
            if (u is null) return Result.NotFound("Unidade não encontrada.");

            u.Restore();
            _repo.Update(u);
            await _uow.SaveChangesAsync(ct);
            return Result.Ok();
        }
    }
}
