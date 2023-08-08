using System.Security.Claims;

namespace Urbamais.WebApi.Shared;

public static class AuthorizeAccess
{
    public static bool Valid(ClaimsPrincipal user, string nameController, char value)
    {
        if (user.IsInRole("developer"))
            return true;

        if (ListPerfis.Instance.Claims!.Any(x => x.Key == nameController.Replace("Controller", "") && x.Value.Contains(value)))
            return true;

        return false;
    }
}