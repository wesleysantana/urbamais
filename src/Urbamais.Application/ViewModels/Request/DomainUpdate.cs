using System.Text.Json.Serialization;

namespace Urbamais.Application.ViewModels.Request;

public abstract class DomainUpdate : IDomainUpdate
{
    [JsonIgnore]
    public string IdUserModification { get; set; } = string.Empty;
}