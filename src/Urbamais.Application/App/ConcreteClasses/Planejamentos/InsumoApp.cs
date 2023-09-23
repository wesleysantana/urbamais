using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;
using System.Net;
using Urbamais.Application.App.Interfaces.Planejamentos;
using Urbamais.Application.Interfaces.Planning;
using Urbamais.Application.Resources;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.V1.Insumo;
using Urbamais.Application.ViewModels.Response;
using Urbamais.Application.ViewModels.Response.V1.Insumo;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.App.ConcreteClasses.Planejamentos;

public class InsumoApp : IInsumoApp
{
    private readonly IInsumoAppService _service;

    public InsumoApp(IInsumoAppService service)
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

    public async Task<Tuple<HttpStatusCode, IValidateViewModel>> Update(object id, IDomainUpdate entity)
    {
        var insumo = await _service.Get(id);
        IValidateViewModel result = new InsumoResponse();

        if (insumo is null)
        {
            result.AddError(ConstantsApp.REGISTER_NOT_FOUND);
            return Tuple.Create(HttpStatusCode.NotFound, result);
        }

        var inputUpdate = entity as InsumoUpdateRequest;

        insumo.Update(inputUpdate?.IdUserModification, inputUpdate?.Nome, inputUpdate?.Descricao, inputUpdate?.UnidadeId, inputUpdate?.Tipo);

        if (!insumo.IsValid)
        {
            result.AddErrors(insumo.ValidationResult!.Errors.Select(x => x.ErrorMessage));
            return Tuple.Create(HttpStatusCode.BadRequest, result);
        }

        _service.Update(insumo);
        if (await Commit() < 1)
            throw new Exception(ConstantsApp.UPDATE_ERROR);

        return Tuple.Create(HttpStatusCode.OK, result);
    }

    public async Task<Tuple<HttpStatusCode, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        var input = await _service.Get(id);
        IValidateViewModel result = new InsumoResponse();

        _service.Delete(id, IdUserDeletion);

        if (input is null)
        {
            result.AddError(ConstantsApp.REGISTER_NOT_FOUND);
            return Tuple.Create(HttpStatusCode.NotFound, result);
        }

        _service.Delete(id, IdUserDeletion);

        if (await Commit() < 1)
            throw new Exception(ConstantsApp.DELETE_ERROR);

        return Tuple.Create(HttpStatusCode.NoContent, result);
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