using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Planning;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Resources;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.v1.Unit;
using Urbamais.Application.ViewModels.Response;
using Urbamais.Application.ViewModels.Response.v1.Unit;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Application.App.ConcreteClasses.Planning;

public class UnitApp : IUnitApp
{
    private readonly IUnitAppService _service;

    public UnitApp(IUnitAppService service)
    {
        _service = service;
    }

    public async Task<Unit> Insert(Unit entity)
    {
        if (entity.IsValid)
        {
            await _service.Insert(entity);
            if (await Commit() < 1)
                throw new Exception(ConstantsApp.INSERT_ERROR);
        }

        return entity;
    }

    public async Task<Tuple<bool, Unit>> Update(object id, IDomainUpdate entity)
    {
        var unit = await _service.Get(id);

        if (unit is null)
            return Tuple.Create(false, (Unit)entity);

        var unitUpdate = entity as UnitUpdateRequest;

        unit.Update(unitUpdate!.IdUserModification, unitUpdate?.Description, unitUpdate?.Acronym);

        if (unit.IsValid)
        {
            _service.Update(unit);
            if (await Commit() < 1)
                throw new Exception(ConstantsApp.UPDATE_ERROR);
        }

        return Tuple.Create(true, unit);
    }

    public async Task<Tuple<bool, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        var unit = await _service.Get(id);
        IValidateViewModel result = new UnitResponse();

        if (unit is null)
        {
            result.AddError(ConstantsApp.REGISTER_NOT_FOUND);
            return Tuple.Create(false, result);
        }

        if (_service.GetInputs(unit.Id).Result.Any())
        {
            result.AddError("Não é possível excluir a unidade pois ela está vinculada a insumo(s) ainda ativo(s).");
            return Tuple.Create(true, result);
        }

        _service.Delete(id, IdUserDeletion);

        if (await Commit() > 0)
            return Tuple.Create(true, result);

        result.AddError(ConstantsApp.DELETE_ERROR);
        return Tuple.Create(true, result);
    }

    public Task<int> Commit() => _service.Commit();

    #region Querys

    public async Task<IList<Unit>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    {
        var query = _service.Query;
        query = query.Apply(filter);

        return await ResultQuery(query, cancellationToken);
    }

    public async Task<Unit> Get(object id) => await _service.Get(id);

    public async Task<Unit> Get(Expression<Func<Unit, bool>> where, CancellationToken cancellationToken) =>
        await _service.Get(where, cancellationToken);

    public async Task<IList<Unit>> List(CancellationToken cancellationToken) => await _service.List(cancellationToken);

    public async Task<IList<Unit>> List(Expression<Func<Unit, bool>> where, CancellationToken cancellationToken) =>
        await _service.List(where, cancellationToken);

    public async Task<IList<Unit>> ResultQuery(IQueryable<Unit> query, CancellationToken cancellationToken) =>
        await _service.ResultQuery(query, cancellationToken);

    public async Task<List<Input>> GetInputs(int unitId) => await _service.GetInputs(unitId);

    #endregion Querys
}