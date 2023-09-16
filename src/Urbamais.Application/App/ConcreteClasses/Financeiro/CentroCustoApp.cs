using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Financeiro;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Response;
using Urbamais.Domain.Entities.Financeiro;

namespace Urbamais.Application.App.ConcreteClasses.Financeiro;

public class CentroCustoApp : ICentroCustoApp
{
    public Task<int> Commit()
    {
        throw new NotImplementedException();
    }

    public Task<Tuple<bool, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        throw new NotImplementedException();
    }

    public Task<CentroCusto> Get(object id)
    {
        throw new NotImplementedException();
    }

    public Task<CentroCusto> Get(Expression<Func<CentroCusto, bool>> where, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CentroCusto> Insert(CentroCusto entity)
    {
        throw new NotImplementedException();
    }

    public Task<IList<CentroCusto>> List(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<CentroCusto>> List(Expression<Func<CentroCusto, bool>> where, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<CentroCusto>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<CentroCusto>> ResultQuery(IQueryable<CentroCusto> query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Tuple<bool, CentroCusto>> Update(object id, IDomainUpdate entity)
    {
        throw new NotImplementedException();
    }
}