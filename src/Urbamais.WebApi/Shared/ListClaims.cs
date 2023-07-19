namespace Urbamais.WebApi.Shared;

internal class ListClaims
{
    private static readonly Lazy<ListClaims> _instance = new(() => new());

    public static ListClaims Instance => _instance.Value;

    public IDictionary<string, string>? Claims;

    private ListClaims()
    {        
    }
}