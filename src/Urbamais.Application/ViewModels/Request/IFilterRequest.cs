using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace Urbamais.Application.ViewModels.Request;

public interface IFilterRequest : ICustomQueryable, IQueryPaging, IQuerySort
{
}