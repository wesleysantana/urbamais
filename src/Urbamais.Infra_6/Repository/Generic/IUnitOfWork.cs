namespace Urbamais.Infra.Repository.Generic;

internal interface IUnitOfWork
{
    Task<int> Commit();
    Task Rollback();
}