using Urbamais.Application.ViewModels.Request.v1.Usuario;
using Urbamais.Application.ViewModels.Response.v1.Usuario;

namespace Urbamais.Application.Interfaces.Identity;

public interface IIdentityAppService
{
    Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);

    Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);

    Task<UsuarioLoginResponse> RefreshLogin(string usuarioId);
}