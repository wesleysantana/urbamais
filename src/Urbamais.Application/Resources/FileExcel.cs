namespace Urbamais.Application.Resources;

public class FileExcel
{
    public string FilePath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;

    public static bool IsValidExcelMimeType(string contentType)
    {
        // Defina os tipos MIME permitidos para arquivos Excel
        // Aqui, estamos permitindo 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
        return contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }

    public static bool IsValidExcelExtension(string filePath)
    {
        // Defina as extensões de arquivo permitidas para arquivos Excel
        // Aqui, estamos permitindo '.xlsx'
        string allowedExtension = ".xlsx";

        // Verifica se a extensão do arquivo corresponde à extensão permitida
        return Path.GetExtension(filePath).Equals(allowedExtension, StringComparison.OrdinalIgnoreCase);
    }
}