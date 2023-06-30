using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Urbamais.Application.ViewModels.Response.Unidade;

public class UnidadeResponse
{
    public int Id { get; }
    public string? Descricao { get; }
    public string? Sigla { get; }
    public bool IsValid { get; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ValidationResult? ValidationResult { get; }
}