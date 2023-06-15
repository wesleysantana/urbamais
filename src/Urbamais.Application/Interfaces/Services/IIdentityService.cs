using Urbamais.Application.ViewModels.Request;
using Urbamais.Application.ViewModels.Response;

namespace Urbamais.Application.Interfaces.Services;

public interface IIdentityService
{
    Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);

    Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);
    Task<UsuarioLoginResponse> RefreshLogin(string usuarioId);
}