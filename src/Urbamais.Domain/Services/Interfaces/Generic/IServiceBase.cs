﻿using System.Linq.Expressions;

namespace Urbamais.Domain.Services.Interfaces.Generic;

public interface IServiceBase<T> where T : class
{
    Task Insert(T entity);

    void Update(T entity);

    void Delete(object id, string IdUserDeletion);

    Task<int> Commit();

    #region Querys

    IQueryable<T> Query { get; }

    Task<T> Get(object id);

    Task<T> Get(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    Task<IList<T>> List(CancellationToken cancellationToken);

    Task<IList<T>> List(Expression<Func<T, bool>> @where, CancellationToken cancellationToken);

    Task<IList<T>> ResultQuery(IQueryable<T> query, CancellationToken cancellationToken);

    #endregion Querys

    void Dispose();
}