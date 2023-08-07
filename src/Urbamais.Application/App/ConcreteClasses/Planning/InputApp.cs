using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Planning;
using Urbamais.Application.Interfaces.Planning;
using Urbamais.Application.Resources;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.V1.Input;
using Urbamais.Application.ViewModels.Response;
using Urbamais.Application.ViewModels.Response.V1.Input;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.App.ConcreteClasses.Planning;

public class InputApp : IInputApp
{
    private readonly IInputAppService _service;

    public InputApp(IInputAppService service)
    {
        _service = service;
    }

    public Task<int> Commit() => _service.Commit();

    public async Task<Insumo> Insert(Insumo entity)
    {
        if (entity.IsValid)
        {
            await _service.Insert(entity);
            if (await Commit() < 1)
                throw new Exception(ConstantsApp.INSERT_ERROR);
        }

        return entity;
    }

    public async Task<Tuple<bool, Insumo>> Update(object id, IDomainUpdate entity)
    {
        var input = await _service.Get(id);

        if (input is null)
            return Tuple.Create(false, (Insumo)entity);

        var inputUpdate = entity as InputUpdateRequest;

        input.Update(inputUpdate?.IdUserModification, inputUpdate?.Name, inputUpdate?.Description, inputUpdate?.UnitId, inputUpdate?.Type);

        if (input.IsValid)
        {
            _service.Update(input);
            if (await Commit() < 1)
                throw new Exception(ConstantsApp.UPDATE_ERROR);
        }

        return Tuple.Create(true, input);
    }

    public async Task<Tuple<bool, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        var input = await _service.Get(id);
        IValidateViewModel result = new InputResponse();

        _service.Delete(id, IdUserDeletion);

        if (input is null)
        {
            result.AddError(ConstantsApp.REGISTER_NOT_FOUND);
            return Tuple.Create(false, result);
        }

        _service.Delete(id, IdUserDeletion);

        if (await Commit() > 0)
            return Tuple.Create(true, result);

        throw new Exception(ConstantsApp.DELETE_ERROR);
    }

    #region Query

    public async Task<Insumo> Get(object id) => await _service.Get(id);

    public async Task<Insumo> Get(Expression<Func<Insumo, bool>> where, CancellationToken cancellationToken) =>
        await _service.Get(where, cancellationToken);

    public async Task<IList<Insumo>> List(CancellationToken cancellationToken) => await _service.List(cancellationToken);

    public async Task<IList<Insumo>> List(Expression<Func<Insumo, bool>> where, CancellationToken cancellationToken) =>
        await _service.List(where, cancellationToken);

    public async Task<IList<Insumo>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    {
        var query = _service.Query;
        query = query.Apply(filter);

        return await ResultQuery(query, cancellationToken);
    }

    public async Task<IList<Insumo>> ResultQuery(IQueryable<Insumo> query, CancellationToken cancellationToken) =>
        await _service.ResultQuery(query, cancellationToken);

    #endregion Query
}