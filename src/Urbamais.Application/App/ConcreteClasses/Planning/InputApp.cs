using AspNetCore.IQueryable.Extensions;
using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Planning;
using Urbamais.Application.Interfaces.Planning;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.v1.Input;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Application.App.ConcreteClasses.Planning;

public class InputApp : IInputApp
{
    private readonly IInputAppService _service;

    public InputApp(IInputAppService service)
    {
        _service = service;
    }

    public Task<int> Commit() => _service.Commit();

    public async Task<Tuple<bool, bool>> Delete(object id, string IdUserDeletion)
    {
        var input = await _service.Get(id);

        if (input is null)
            return Tuple.Create(false, false);

        _service.Delete(id, IdUserDeletion);

        if (await Commit() > 0)
            return Tuple.Create(true, true);

        return Tuple.Create(true, false);
    }

    public async Task<Input> Get(object id) => await _service.Get(id);

    public async Task<Input> Get(Expression<Func<Input, bool>> where, CancellationToken cancellationToken) =>
        await _service.Get(where, cancellationToken);

    public async Task<Input> Insert(Input entity)
    {
        if (entity.IsValid)
        {
            await _service.Insert(entity);
            await Commit();
        }

        return entity;
    }

    public async Task<IList<Input>> List(CancellationToken cancellationToken) => await _service.List(cancellationToken);

    public async Task<IList<Input>> List(Expression<Func<Input, bool>> where, CancellationToken cancellationToken) =>
        await _service.List(where, cancellationToken);

    public async Task<IList<Input>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    {
        var query = _service.Query;
        query = query.Apply(filter);

        return await ResultQuery(query, cancellationToken);
    }

    public async Task<IList<Input>> ResultQuery(IQueryable<Input> query, CancellationToken cancellationToken) =>
        await _service.ResultQuery(query, cancellationToken);

    public async Task<Tuple<bool, Input>> Update(object id, IDomainUpdate entity)
    {
        var input = await _service.Get(id);

        if (input is null)
            return Tuple.Create(false, (Input)entity);

        var InputUpdate = entity as InputUpdateRequest;

        input.Update()

        if (input.IsValid)
        {
            _service.Update(input);
            await Commit();
        }

        return Tuple.Create(true, input);
    }
}