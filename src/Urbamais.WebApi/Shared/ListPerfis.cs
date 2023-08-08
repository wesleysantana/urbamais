namespace Urbamais.WebApi.Shared;

internal class ListPerfis
{
    private static readonly Lazy<ListPerfis> _instance = new(() => new());

    public static ListPerfis Instance => _instance.Value;

    public IDictionary<string, string>? Claims = new Dictionary<string, string>();

    private ListPerfis()
    {        
    }
}