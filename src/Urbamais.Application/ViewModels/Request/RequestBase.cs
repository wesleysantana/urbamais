using System.Text.Json.Serialization;

namespace Urbamais.Application.ViewModels.Request;

public abstract class RequestBase
{
    [JsonIgnore]
    public string IdUserCreation { get; set; } = string.Empty;
}