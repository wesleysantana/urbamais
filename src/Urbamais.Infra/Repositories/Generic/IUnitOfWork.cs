namespace Urbamais.Infra.Repositories.Generic;

internal interface IUnitOfWork
{
    Task<int> Commit();
    Task Rollback();
}