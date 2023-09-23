using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Linq.Expressions;
using System.Net;
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

    public Task<Tuple<HttpStatusCode, IValidateViewModel>> Delete(object id, string IdUserDeletion)
    {
        throw new NotImplementedException();
    }

    public async Task<RegistroFinanceiro> Get(object id) => await _service.Get(id);

    public async Task<RegistroFinanceiro> Get(Expression<Func<RegistroFinanceiro, bool>> where, CancellationToken cancellationToken) =>
        await _service.Get(where, cancellationToken);

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

    public Task<Tuple<HttpStatusCode, IValidateViewModel>> Update(object id, IDomainUpdate entity)
    {
        throw new NotImplementedException();
    }

    public async Task<object> ReadExcelAsync(FileExcel fileExcel, int lineHeader = 4)
    {
        List<string> header = new();
        List<List<string>> data = new();

        using (FileStream fs = new(fileExcel.FilePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook workbook = new XSSFWorkbook(fs); // Para arquivos .xlsx
            ISheet worksheet = workbook.GetSheetAt(0); // Pode ser por nome: workbook.GetSheet("NomeDaPlanilha")

            // Ler o cabeçalho da primeira linha da planilha
            IRow headerRow = worksheet.GetRow(lineHeader);
            for (int i = 0; i < headerRow.LastCellNum; i++)
            {
                ICell cell = headerRow.GetCell(i);
                header.Add(cell.StringCellValue);
            }

            // Ler os dados a partir da segunda linha
            for (int rowIndex = lineHeader + 1; rowIndex <= worksheet.LastRowNum; rowIndex++)
            {
                IRow currentRow = worksheet.GetRow(rowIndex);
                if (currentRow is null) break;
                List<string> rowData = new();

                for (int colIndex = 0; colIndex < header.Count; colIndex++)
                {
                    ICell cell = currentRow.GetCell(colIndex);
                    if (cell != null)
                    {
                        rowData.Add(cell.ToString()!);
                    }
                    else
                    {
                        rowData.Add(string.Empty);
                    }
                }

                data.Add(rowData);
            }
        }

        // Agora você tem o cabeçalho em 'header' e os dados em 'data'

        // Exemplo de como imprimir o cabeçalho
        Console.WriteLine("Cabeçalho:");
        foreach (var column in header)
        {
            Console.Write(column + "\t");
        }
        Console.WriteLine();

        // Exemplo de como imprimir os dados
        foreach (var row in data)
        {
            foreach (var cell in row)
            {
                Console.Write(cell + "\t");
            }
            Console.WriteLine();
        }

        return new
        {
            Ok = true
        };
    }
}