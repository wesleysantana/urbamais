using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Net;
using Urbamais.Application.App.Interfaces.Core;
using Urbamais.Application.App.Interfaces.Financeiro;
using Urbamais.Application.Resources;
using Urbamais.WebApi.Shared;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class RegistroFinanceiroController : ControllerBase
{
    private readonly IRegistroFinanceiroApp _registroFinanceiroApp;
    private readonly IMapper _mapper;

    public RegistroFinanceiroController(IRegistroFinanceiroApp registroFinanceiroApp, IMapper mapper)
    {
        _registroFinanceiroApp = registroFinanceiroApp;
        _mapper = mapper;
    }

    [HttpPost("read-excel")]
    public async Task<IActionResult> ReadExcelAsync([FromBody] FileExcel request, int lineHeader = 4)
    {
        try
        {
            if (!AuthorizeAccess.Valid(User, GetType().Name, Constants.CREATE))
                return Unauthorized();

            if (string.IsNullOrEmpty(request.FilePath))            
                return BadRequest(new CustomProblemDetails(HttpStatusCode.BadRequest, 
                    detail: "O caminho do arquivo Excel não foi especificado."));

            _ = _registroFinanceiroApp.ReadExcelAsync(request, lineHeader);

            FileInfo fileInfo = new(request.FilePath);            

            return Ok();
            
        }
        catch (Exception ex)
        {
            return BadRequest(new CustomProblemDetails(HttpStatusCode.InternalServerError,
                   detail: $"Erro ao ler o arquivo Excel: {ex.Message}"));
        }
    }
}