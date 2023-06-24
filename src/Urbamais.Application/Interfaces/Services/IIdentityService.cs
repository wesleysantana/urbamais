using Urbamais.Application.ViewModels.Request.Usuario;
using Urbamais.Application.ViewModels.Response.Usuario;

namespace Urbamais.Application.Interfaces.Services;

public interface IIdentityService
{
    Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);

    Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);

    Task<UsuarioLoginResponse> RefreshLogin(string usuarioId);
}