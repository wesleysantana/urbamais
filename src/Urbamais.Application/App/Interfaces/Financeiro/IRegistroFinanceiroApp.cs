using Urbamais.Application.App.Interfaces.Generic;
using Urbamais.Application.Resources;
using Urbamais.Domain.Entities.Financeiro;

namespace Urbamais.Application.App.Interfaces.Financeiro;

public interface IRegistroFinanceiroApp : IApp<RegistroFinanceiro>
{
    Task<object> ReadExcelAsync(FileExcel fileExcel, int lineHeader = 4);
}