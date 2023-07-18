using Microsoft.AspNetCore.Mvc;

namespace Urbamais.WebApi.ControllersHelper;

internal class ListControllers
{
    private static readonly Lazy<ListControllers> _instance = new(() => new());

    public static ListControllers Instance => _instance.Value;

    public IList<string> List { get; set; }

    private ListControllers()
    {
        List = new List<string>();
    }
}