using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Planejamentos;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Planejamentos;

namespace Urbamais.Domain.Services.ConcreteClasses.Planejamentos;

public class UnidadeService : ServiceBase<Unidade>, IUnidadeService
{
    private readonly IUnidadeRepository _unidadeRepository;

    public UnidadeService(IUnidadeRepository repository) : base(repository)
    {
        _unidadeRepository = repository;
    }

    public Task<List<Insumo>> GetInsumos(int unitId) =>
        _unidadeRepository.GetInputs(unitId);
}