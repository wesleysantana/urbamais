using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;
using System.Net;
using Urbamais.Application.App.Interfaces.Planejamentos;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.Resources;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.V1.Unidade;
using Urbamais.Application.ViewModels.Response;
using Urbamais.Application.ViewModels.Response.V1.Insumo;
using Urbamais.Application.ViewModels.Response.V1.Unidade;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.Application.App.ConcreteClasses.Planejamentos;

public class UnidadeApp : IUnidadeApp
{
    private readonly IUnidadeAppService _service;

    public UnidadeApp(IUnidadeAppService service)
    {
        _service = service;
    }

    public async Task<Unidade> Insert(Unidade entity)
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
        var unidade = await _service.Get(id);
        IValidateViewModel result = new InsumoResponse();

        if (unidade is null)
        {
            result.AddError(ConstantsApp.REGISTER_NOT_FOUND);
            return Tuple.Create(HttpStatusCode.NotFound, result);
        }

        var unitUpdate = entity as UnidadeUpdateRequest;

        unidade.Update(unitUpdate!.IdUserModification, unitUpdate?.Descricao, unitUpdate?.Sigla);

        if (!unidade.IsValid)
        {
            result.AddErrors(unidade.ValidationResult!.Errors.Select(x => x.ErrorMessage));
            return Tuple.Create(HttpStatusCode.BadRequest, result);           
        }

        _service.Update(unidade);
        if (await Commit() < 1)
            throw new Exception(ConstantsApp.UPDATE_ERROR);

        return Tuple.Create(HttpStatusCode.OK, result);
    }

    public async Task<Tuple<HttpStatusCode, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        var unidade = await _service.Get(id);
        IValidateViewModel result = new UnidadeResponse();

        if (unidade is null)
        {
            result.AddError(ConstantsApp.REGISTER_NOT_FOUND);
            return Tuple.Create(HttpStatusCode.NotFound, result);
        }

        if (_service.GetInsumos(unidade.Id).Result.Any())
        {
            result.AddError("Não é possível excluir a unidade pois ela está vinculada a insumo(s) ainda ativo(s).");
            return Tuple.Create(HttpStatusCode.BadRequest, result);
        }

        _service.Delete(id, IdUserDeletion);

        if (await Commit() < 1)
            throw new Exception(ConstantsApp.DELETE_ERROR);

        return Tuple.Create(HttpStatusCode.NoContent, result);
    }

    public Task<int> Commit() => _service.Commit();

    #region Querys

    public async Task<IList<Unidade>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    {
        var query = _service.Query;
        query = query.Apply(filter);

        return await ResultQuery(query, cancellationToken);
    }

    public async Task<Unidade> Get(object id) => await _service.Get(id);

    public async Task<Unidade> Get(Expression<Func<Unidade, bool>> where, CancellationToken cancellationToken) =>
        await _service.Get(where, cancellationToken);

    public async Task<IList<Unidade>> List(CancellationToken cancellationToken) => await _service.List(cancellationToken);

    public async Task<IList<Unidade>> List(Expression<Func<Unidade, bool>> where, CancellationToken cancellationToken) =>
        await _service.List(where, cancellationToken);

    public async Task<IList<Unidade>> ResultQuery(IQueryable<Unidade> query, CancellationToken cancellationToken) =>
        await _service.ResultQuery(query, cancellationToken);

    public async Task<List<Insumo>> GetInsumos(int unitId) => await _service.GetInsumos(unitId);

    #endregion Querys
}