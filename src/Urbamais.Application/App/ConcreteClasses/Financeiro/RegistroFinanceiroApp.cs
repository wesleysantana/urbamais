using OfficeOpenXml;
using Urbamais.Application.Resources;

namespace Urbamais.Application.App.ConcreteClasses.Financeiro;

public class RegistroFinanceiroApp
{
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