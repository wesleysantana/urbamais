using Urbamais.Application.ViewModels.Request.v1.User;
using Urbamais.Application.ViewModels.Response.v1.User;

namespace Urbamais.Application.Interfaces.Identity;

public interface IIdentityAppService
{
    Task<UserRegisterResponse> RegisterUser(UserRegisterRequest usuarioCadastro);

    Task<UsuarioLoginResponse> Login(UserLoginRequest usuarioLogin);

    Task<UsuarioLoginResponse> RefreshLogin(string usuarioId);
}