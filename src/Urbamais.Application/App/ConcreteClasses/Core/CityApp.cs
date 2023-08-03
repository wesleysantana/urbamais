using AspNetCore.IQueryable.Extensions;
using Core.Domain;
using Core.ValueObjects;
using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Core;
using Urbamais.Application.Interfaces.Core;
using Urbamais.Application.Resources;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Request.V1.City;
using Urbamais.Application.ViewModels.Response;
using Urbamais.Application.ViewModels.Response.V1.City;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Application.App.ConcreteClasses.Core;

public class CityApp : ICityApp
{
    private readonly ICityAppService _service;

    public CityApp(ICityAppService service)
    {
        _service = service;
    }

    public Task<int> Commit() => _service.Commit();

    public async Task<City> Insert(City entity)
    {
        if (entity.IsValid)
        {
            await _service.Insert(entity);
            if (await Commit() < 1)
                throw new Exception(ConstantsApp.INSERT_ERROR);
        }

        return entity;
    }

    public async Task<Tuple<bool, City>> Update(object id, IDomainUpdate entity)
    {
        var city = await _service.Get(id);

        if (city is null)
            return Tuple.Create(false, (City)entity);

        var cityUpdate = entity as CityUpdateRequest;

        var name = new Name(cityUpdate!.Name!);
        _ = Enum.TryParse(cityUpdate!.Name!, out Uf uf);
        city.Update(cityUpdate.IdUserModification, name, uf);

        if (city.IsValid)
        {
            _service.Update(city);
            if (await Commit() < 1)
                throw new Exception(ConstantsApp.UPDATE_ERROR);
        }

        return Tuple.Create(true, city);
    }

    public async Task<Tuple<bool, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        var city = await _service.Get(id);
        IValidateViewModel result = new CityResponse();

        _service.Delete(id, IdUserDeletion);

        if (city is null)
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

    public async Task<City> Get(object id) => await _service.Get(id);

    public async Task<City> Get(Expression<Func<City, bool>> where, CancellationToken cancellationToken) =>
        await _service.Get(where, cancellationToken);

    public async Task<IList<City>> List(CancellationToken cancellationToken) => await _service.List(cancellationToken);

    public async Task<IList<City>> List(Expression<Func<City, bool>> where, CancellationToken cancellationToken) =>
        await _service.List(where, cancellationToken);

    public async Task<IList<City>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    {
        var query = _service.Query;
        query = query.Apply(filter);

        return await ResultQuery(query, cancellationToken);
    }

    //public async Task<IList<City>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    //{
    //    var pbClienteContato = PredicateBuilder.True<City>();
    //    CityFilterRequest? cityFilter = filter as CityFilterRequest;

    //    if (!string.IsNullOrWhiteSpace(cityFilter!.Name))
    //        pbClienteContato = pbClienteContato.And(x => x.Name.Name!.ToLower().Contains(cityFilter.Name.ToLower()));

    //    var lista = _service.List(pbClienteContato, cancellationToken);

    //    return await lista;
    //}

    public async Task<IList<City>> ResultQuery(IQueryable<City> query, CancellationToken cancellationToken) =>
        await _service.ResultQuery(query, cancellationToken);

    //public async Task<List<City>> ListarPaginado(int pagina, int tamanhoPagina, Expression<Func<Cliente, bool>> predicate)
    //{
    //    var pular = (pagina - 1) * tamanhoPagina;

    //    var clientes = _context.Clientes.Include(c => c.Contatos).Where(predicate).OrderBy(c => c.Nome).Skip(pular).Take(tamanhoPagina).Select(c => new Cliente
    //    {
    //        CustomerSuccess = c.CustomerSuccess,
    //        DataCriacao = c.DataCriacao,
    //        DataEdicao = c.DataEdicao,
    //        IdCustomerSuccess = c.IdCustomerSuccess,
    //        Nome = c.Nome,
    //        ResponsavelCriacao = c.ResponsavelCriacao,
    //        ResponsavelEdicao = c.ResponsavelEdicao,
    //        Setor = c.Setor,
    //        SetorId = c.SetorId,
    //        Nomeada = c.Nomeada,
    //        ServicosAtivos = c.ServicosAtivos,
    //        Solicitacoes = c.Solicitacoes,
    //        Telefone = c.Telefone,
    //        Id = c.Id,
    //        Contatos = c.Contatos
    //    }).ToList();

    //    return clientes;
    //}

    #endregion Query
}