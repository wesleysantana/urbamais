using System.Security.Claims;

namespace Urbamais.WebApi.Shared;

public static class AuthorizeAccess
{
    public static bool Valid(ClaimsPrincipal user, string nameController, char value)
    {
        if (user.IsInRole("developer"))
            return true;

        if (ListClaims.Instance.Claims!.Any(x => x.Key == nameController && x.Value.Contains(value)))
            return true;

        return false;
    }
}