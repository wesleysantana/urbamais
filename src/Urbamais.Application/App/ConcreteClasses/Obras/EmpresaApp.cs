using AspNetCore.IQueryable.Extensions;
using Core.ValueObjects;
using System.Linq.Expressions;
using System.Net;
using Urbamais.Application.App.Interfaces.Obras;
using Urbamais.Application.Interfaces.Obras;
using Urbamais.Application.Resources;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.V1.Empresa;
using Urbamais.Application.ViewModels.Response;
using Urbamais.Application.ViewModels.Response.V1.Empresa;
using Urbamais.Domain.Entities.Obras;

namespace Urbamais.Application.App.ConcreteClasses.Obras;

public class EmpresaApp : IEmpresaApp
{
    private readonly IEmpresaAppService _service;

    public EmpresaApp(IEmpresaAppService service)
    {
        _service = service;
    }

    public Task<int> Commit() => _service.Commit();

    public async Task<Empresa> Insert(Empresa entity)
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
        var empresa = await _service.Get(id);
        IValidateViewModel result = new EmpresaResponse();

        if (empresa is null)
        {
            result.AddError(ConstantsApp.REGISTER_NOT_FOUND);
            return Tuple.Create(HttpStatusCode.NotFound, result);
        }

        var companieUpdate = entity as EmpresaUpdateRequest;

        var corporateName = new Nome(companieUpdate!.RazaoSocial!);
        var tradeName = new Nome(companieUpdate!.NomeFantasia!);
        var cnpj = new Cnpj(companieUpdate!.Cnpj!);
        empresa.Update(companieUpdate!.IdUserModification, corporateName, tradeName,
            cnpj, companieUpdate.Enderecos, companieUpdate.Telefones, companieUpdate.Emails);

        if (!empresa.IsValid)
        {
            result.AddErrors(empresa.ValidationResult!.Errors.Select(x => x.ErrorMessage));
            return Tuple.Create(HttpStatusCode.BadRequest, result);
        }

        _service.Update(empresa);
        if (await Commit() < 1)
            throw new Exception(ConstantsApp.UPDATE_ERROR);

        return Tuple.Create(HttpStatusCode.OK, result);
    }

    public async Task<Tuple<HttpStatusCode, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        var Companie = await _service.Get(id);
        IValidateViewModel result = new EmpresaResponse();

        _service.Delete(id, IdUserDeletion);

        if (Companie is null)
        {
            result.AddError(ConstantsApp.REGISTER_NOT_FOUND);
            return Tuple.Create(HttpStatusCode.NotFound, result);
        }

        _service.Delete(id, IdUserDeletion);

        if (await Commit() < 0)
            throw new Exception(ConstantsApp.DELETE_ERROR);

        return Tuple.Create(HttpStatusCode.NoContent, result);
    }

    #region Query

    public async Task<Empresa> Get(object id) => await _service.Get(id);

    public async Task<Empresa> Get(Expression<Func<Empresa, bool>> where, CancellationToken cancellationToken) =>
        await _service.Get(where, cancellationToken);

    public async Task<IList<Empresa>> List(CancellationToken cancellationToken) => await _service.List(cancellationToken);

    public async Task<IList<Empresa>> List(Expression<Func<Empresa, bool>> where, CancellationToken cancellationToken) =>
        await _service.List(where, cancellationToken);

    public async Task<IList<Empresa>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    {
        var query = _service.Query;
        query = query.Apply(filter);

        return await ResultQuery(query, cancellationToken);
    }

    public async Task<IList<Empresa>> ResultQuery(IQueryable<Empresa> query, CancellationToken cancellationToken) =>
        await _service.ResultQuery(query, cancellationToken);

    #endregion Query
}