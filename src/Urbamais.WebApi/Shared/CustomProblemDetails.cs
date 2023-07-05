using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Urbamais.WebApi.Shared;

public class CustomProblemDetails : ProblemDetails
{
    public List<string> Errors { get; private set; }

    public CustomProblemDetails(HttpStatusCode status, string? detail = null, IEnumerable<string>? errors = null) : this()
    {
        Title = status switch
        {
            HttpStatusCode.BadRequest => "Ocorreu um ou mais erros de validação.",
            HttpStatusCode.InternalServerError => "Erro do Servidor Interno.",
            HttpStatusCode.NotFound => "O item solicitado não foi encontrado.",
            _ => "Ocorreu um erro."
        };

        Status = (int)status;
        Detail = detail;

        if (errors is not null)
        {
            if (errors.Count() == 1)
                Detail = errors.First();
            else if (errors.Count() > 1)
                Detail = "Vários problemas ocorreram.";

            Errors.AddRange(errors);
        }
    }

    public CustomProblemDetails(HttpStatusCode status, HttpRequest request, string? detail = null, IEnumerable<string>? errors = null) 
        : this(status, detail, errors) =>
        Instance = request.Path;

    private CustomProblemDetails() =>
        Errors = new List<string>();
}
