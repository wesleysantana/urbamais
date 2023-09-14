using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Net;
using Urbamais.Application.Resources;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class ContaController : ControllerBase
{
    [HttpPost("read-excel")]
    public async Task<IActionResult> ReadExcelAsync([FromBody] FileExcel request)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.CREATE))
                return Unauthorized();

            if (string.IsNullOrEmpty(request.FilePath))            
                return BadRequest(new CustomProblemDetails(HttpStatusCode.BadRequest, 
                    detail: "O caminho do arquivo Excel não foi especificado."));            

            FileInfo fileInfo = new FileInfo(request.FilePath);            

            return Ok();
            
        }
        catch (Exception ex)
        {
            return BadRequest(new CustomProblemDetails(HttpStatusCode.InternalServerError,
                   detail: $"Erro ao ler o arquivo Excel: {ex.Message}"));
        }
    }
}