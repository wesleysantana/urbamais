using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Planejamento;
using Urbamais.Application.Interfaces.Planejamento;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.Application.App.ConcreteClasses.Planejamento;

public class UnidadeApp : IUnidadeApp
{
    private readonly IUnidadeAppService _service;

    public UnidadeApp(IUnidadeAppService service)
    {
        _service = service;
    }

    public IQueryable<Unidade> Query => throw new NotImplementedException();

    public Task<int> Commit() => _service.Commit();    

    public void Delete(Unidade entity)
    {
        throw new NotImplementedException();
    }

    public Task<Unidade> Get(object id)
    {
        throw new NotImplementedException();
    }

    public Task<Unidade> Get(Expression<Func<Unidade, bool>> where)
    {
        throw new NotImplementedException();
    }

    public async Task<Unidade> Insert(Unidade entity)
    {
        if (entity.IsValid)
        {
            await _service.Insert(entity);
            _ = Commit();
        }
        
        return entity;
    }

    public async Task<IList<Unidade>> List() => await _service.List();

    public Task<IList<Unidade>> List(Expression<Func<Unidade, bool>> where)
    {
        throw new NotImplementedException();
    }

    public Task<Unidade> Update(Unidade entity)
    {
        throw new NotImplementedException();
    }
}