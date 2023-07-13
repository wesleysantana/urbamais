using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Planning;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.v1.Unit;
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
            await Commit();
        }

        return entity;
    }

    public async Task<Tuple<bool, Unit>> Update(object id, IDomainUpdate entity)
    {
        var unit = await _service.Get(id);

        if (unit is null)
            return Tuple.Create(false, (Unit)entity);

        var unitUpdate = entity as UnitUpdateRequest;

        unit.Update(unitUpdate?.Description, unitUpdate?.Acronym);

        if (unit.IsValid)
        {
            _service.Update(unit);
            await Commit();
        }

        return Tuple.Create(true, unit);
    }

    public async Task<Tuple<bool, bool>> Delete(object id)
    {
        var unit = await _service.Get(id);

        if (unit is null)
            return Tuple.Create(false, false);

        _service.Delete(id);

        if (await Commit() > 0)
            return Tuple.Create(true, true);

        return Tuple.Create(true, false);
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

    #endregion Querys
}