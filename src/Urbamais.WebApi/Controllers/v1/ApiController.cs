using Microsoft.AspNetCore.Mvc;
using Urbamais.WebApi.ControllersHelper;

namespace Urbamais.WebApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    [HttpGet]
    public List<string> Get()
    {
        return ListControllers.Instance.List.ToList();
    }
}