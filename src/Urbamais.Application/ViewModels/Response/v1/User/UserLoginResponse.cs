using System.Text.Json.Serialization;

namespace Urbamais.Application.ViewModels.Response.v1.User;

public class UserLoginResponse
{
    public bool Success => Errors.Count == 0;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string AccessToken { get; private set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string RefreshToken { get; private set; } = string.Empty;

    public List<string> Errors { get; private set; }

    public UserLoginResponse() => Errors = new List<string>();

    public UserLoginResponse(string accessToken, string refreshToken) : this()
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public void AddError(string error) => Errors.Add(error);

    public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
}