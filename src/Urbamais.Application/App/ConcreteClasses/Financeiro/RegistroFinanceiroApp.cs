using OfficeOpenXml;
using System.Linq.Expressions;
using Urbamais.Application.App.Interfaces.Financeiro;
using Urbamais.Application.Interfaces.Financeiro;
using Urbamais.Application.Resources;
using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Response;
using Urbamais.Domain.Entities.Financeiro;

namespace Urbamais.Application.App.ConcreteClasses.Financeiro;

public class RegistroFinanceiroApp : IRegistroFinanceiroApp
{
    private readonly IRegistroFinanceiroAppService _service;

    public RegistroFinanceiroApp(IRegistroFinanceiroAppService service)
    {
        _service = service;
    }

    public Task<int> Commit() => _service.Commit();

    public Task<Tuple<bool, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        throw new NotImplementedException();
    }

    public Task<RegistroFinanceiro> Get(object id)
    {
        throw new NotImplementedException();
    }

    public Task<RegistroFinanceiro> Get(Expression<Func<RegistroFinanceiro, bool>> where, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<RegistroFinanceiro> Insert(RegistroFinanceiro entity)
    {
        if (entity.IsValid)
        {
            await _service.Insert(entity);
            if (await Commit() < 1)
                throw new Exception(ConstantsApp.INSERT_ERROR);
        }

        return entity;
    }

    public Task<IList<RegistroFinanceiro>> List(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<RegistroFinanceiro>> List(Expression<Func<RegistroFinanceiro, bool>> where, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<RegistroFinanceiro>> Query(IFilterRequest filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<RegistroFinanceiro>> ResultQuery(IQueryable<RegistroFinanceiro> query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Tuple<bool, RegistroFinanceiro>> Update(object id, IDomainUpdate entity)
    {
        throw new NotImplementedException();
    }

    public async Task<object> ReadExcelAsync(FileExcel fileExcel)
    {
        FileInfo fileInfo = new(fileExcel.FilePath);

        using var package = new ExcelPackage(fileInfo);
        var worksheet = package.Workbook.Worksheets[0];

        // Ler as células e processar os dados aqui
        // Por exemplo, você pode retornar os dados lidos como JSON
        // ou executar alguma lógica de negócios com eles

        // Exemplo: ler as células e retornar como JSON

        return new
        {
            worksheet.Dimension.Rows,
            worksheet.Dimension.Columns,
            Values = worksheet.Cells.Value
        };
    }
}