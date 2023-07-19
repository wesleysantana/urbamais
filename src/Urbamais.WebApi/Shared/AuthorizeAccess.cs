namespace Urbamais.WebApi.Shared;

public static class AuthorizeAccess
{
    public static bool Valid(string nameController, char value)
    {
        if (ListClaims.Instance.Claims!.Any(x => x.Key == nameController && x.Value.Contains(value)))
            return true;

        return false;
    }
}
