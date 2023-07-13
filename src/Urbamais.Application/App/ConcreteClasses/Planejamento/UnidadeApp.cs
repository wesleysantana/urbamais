using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Planejamento;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.Application.App.ConcreteClasses.Planejamento;

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
            await Commit();
        }

        return entity;
    }

    public async Task<Tuple<bool, Unidade>> Update(object id, IDomainUpdate entity)
    {
        var unidade = await _service.Get(id);

        if (unidade is null)
            return Tuple.Create(false, (Unidade)entity);

        var unidadeUpdate = entity as UnidadeUpdateRequest;

        unidade.Update(unidadeUpdate?.Descricao, unidadeUpdate?.Sigla);

        if (unidade.IsValid)
        {
            _service.Update(unidade);
            await Commit();
        }

        return Tuple.Create(true, unidade);
    }

    public async Task<Tuple<bool, bool>> Delete(object id)
    {
        var unidade = await _service.Get(id);

        if (unidade is null)
            return Tuple.Create(false, false);

        _service.Delete(id);

        if (await Commit() > 0)
            return Tuple.Create(true, true);

        return Tuple.Create(true, false);
    }

    public Task<int> Commit() => _service.Commit();

    #region Querys

    public async Task<IList<Unidade>> Query(IFiltroRequest filtro, CancellationToken cancellationToken)
    {
        var query = _service.Query;
        query = query.Apply(filtro);

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

    #endregion Querys
}