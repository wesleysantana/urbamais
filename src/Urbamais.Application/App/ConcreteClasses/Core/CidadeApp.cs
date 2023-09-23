using AspNetCore.IQueryable.Extensions;
using Core.Domain;
using Core.ValueObjects;
using System.Linq.Expressions;
using System.Net;
using Urbamais.Application.App.Interfaces.Core;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Application.Resources;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.V1.Cidade;
using Urbamais.Application.ViewModels.Response;
using Urbamais.Application.ViewModels.Response.V1.Cidade;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Application.App.ConcreteClasses.Core;

public class CidadeApp : ICidadeApp
{
    private readonly ICidadeAppService _service;

    public CidadeApp(ICidadeAppService service)
    {
        _service = service;
    }

    public Task<int> Commit() => _service.Commit();

    public async Task<Cidade> Insert(Cidade entity)
    {
        if (entity.IsValid)
        {
            await _service.Insert(entity);
            if (await Commit() < 1)
                throw new Exception(ConstantsApp.INSERT_ERROR);
        }

        return entity;
    }

    //public async Task<Tuple<HttpStatusCode, Cidade>> Update(object id, IDomainUpdate entity)
    //{
    //    var cidade = await _service.Get(id);

    //    if (cidade is null)
    //        return Tuple.Create(HttpStatusCode.NotFound, (Cidade)entity);

    //    var cityUpdate = entity as CidadeUpdateRequest;

    //    var name = new Nome(cityUpdate!.Nome!);

    //    if(!Enum.TryParse(cityUpdate!.Nome!, out Uf uf))
    //    {
    //        var cidadeError = (Cidade)entity;
    //        cidadeError.ValidationResult?.Errors.Add(new FluentValidation.Results.ValidationFailure
    //        {
    //            ErrorMessage = "Uf não encontrada"
    //        });
    //        return Tuple.Create(HttpStatusCode.BadRequest, cidadeError);
    //    }

    //    cidade.Update(cityUpdate.IdUserModification, name, uf);

    //    if (cidade.IsValid)
    //    {
    //        _service.Update(cidade);
    //        if (await Commit() < 1)
    //            throw new Exception(ConstantsApp.UPDATE_ERROR);
    //    }

    //    return Tuple.Create(HttpStatusCode.OK, cidade);
    //}

    public async Task<Tuple<HttpStatusCode, IValidateViewModel>> Update(object id, IDomainUpdate entity)
    {
        var cidade = await _service.Get(id);
        IValidateViewModel result = new CidadeResponse();

        if (cidade is null)
        {
            result.AddError(ConstantsApp.REGISTER_NOT_FOUND);
            return Tuple.Create(HttpStatusCode.NotFound, result);
        }

        var cityUpdate = entity as CidadeUpdateRequest;       

        if (!Enum.TryParse(cityUpdate!.Nome!, out Uf uf))
        {
            result.AddError("Uf não encontrada");            
            return Tuple.Create(HttpStatusCode.BadRequest, result);
        }

        var nome = new Nome(cityUpdate!.Nome!);

        cidade.Update(cityUpdate.IdUserModification, nome, uf);

        if (!cidade.IsValid)
        {
            result.AddErrors(cidade.ValidationResult!.Errors.Select(x => x.ErrorMessage));
            return Tuple.Create(HttpStatusCode.BadRequest, result);           
        }

        _service.Update(cidade);
        if (await Commit() < 1)
            throw new Exception(ConstantsApp.UPDATE_ERROR);

        return Tuple.Create(HttpStatusCode.OK, result);
    }

    public async Task<Tuple<HttpStatusCode, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        var cidade = await _service.Get(id);
        IValidateViewModel result = new CidadeResponse();

        _service.Delete(id, IdUserDeletion);

        if (cidade is null)
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

    public async Task<Cidade> Get(object id) => await _service.Get(id);

    public async Task<Cidade> Get(Expression<Func<Cidade, bool>> where, CancellationToken cancellationToken) =>
        await _service.Get(where, cancellationToken);

    public async Task<IList<Cidade>> List(CancellationToken cancellationToken) => await _service.List(cancellationToken);

    public async Task<IList<Cidade>> List(Expression<Func<Cidade, bool>> where, CancellationToken cancellationToken) =>
        await _service.List(where, cancellationToken);

    public async Task<IList<Cidade>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    {
        var query = _service.Query;
        query = query.Apply(filter);

        return await ResultQuery(query, cancellationToken);
    }

    public async Task<IList<Cidade>> ResultQuery(IQueryable<Cidade> query, CancellationToken cancellationToken) =>
        await _service.ResultQuery(query, cancellationToken);    

    #endregion Query
}