using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Planejamento;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.Unidade;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.Application.App.ConcreteClasses.Planejamento;

public class UnidadeApp : IUnidadeApp
{
    private readonly IUnidadeAppService _service;

    public UnidadeApp(IUnidadeAppService service)
    {
        _service = service;
    }

    public IQueryable<Unidade> Query => _service.Query;

    public Task<int> Commit() => _service.Commit();

    public Task<Unidade> Get(object id) => _service.Get(id);

    public Task<Unidade> Get(Expression<Func<Unidade, bool>> where) => _service.Get(where);

    public async Task<Unidade> Insert(Unidade entity)
    {
        if (entity.IsValid)
        {
            await _service.Insert(entity);
            await Commit();
        }

        return entity;
    }

    public async Task<IList<Unidade>> List() => await _service.List();

    public Task<IList<Unidade>> List(Expression<Func<Unidade, bool>> where) => _service.List(where);

    public async Task<Tuple<bool, Unidade>> Update(object id, IDomainUpdate entity)
    {
        var unidade = await _service.Get(id);

        if (unidade is null)
            return Tuple.Create(false, (Unidade)entity);

        unidade.Update(((UnidadeUpdateRequest)entity).Descricao, ((UnidadeUpdateRequest)entity).Sigla);

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
        await Commit();

        return Tuple.Create(true, true);
    }
}