using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Planning;

namespace Urbamais.Domain.Services.ConcreteClasses.Planejamento;

public class UnitService : ServiceBase<Unidade>, IUnitService
{
    private readonly IUnitRepository _unitRepository;

    public UnitService(IUnitRepository repository) : base(repository)
    {
        _unitRepository = repository;
    }

    public Task<List<Insumo>> GetInputs(int unitId) =>
        _unitRepository.GetInputs(unitId);
}