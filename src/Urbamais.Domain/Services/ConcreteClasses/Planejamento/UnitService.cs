using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.InterfacesRepositories.Planejamento;
using Urbamais.Domain.Services.ConcreteClasses.Generic;
using Urbamais.Domain.Services.Interfaces.Planning;

namespace Urbamais.Domain.Services.ConcreteClasses.Planejamento;

public class UnitService : ServiceBase<Unit>, IUnitService
{
    private readonly IUnitRepository _unitRepository;

    public UnitService(IUnitRepository repository) : base(repository)
    {
        _unitRepository = repository;
    }

    public Task<List<Input>> GetInputs(int unitId) =>
        _unitRepository.GetInputs(unitId);
}