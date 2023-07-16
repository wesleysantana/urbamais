using Urbamais.Application.ViewModels.Request.v1.Role;
using Urbamais.Application.ViewModels.Request.v1.User;
using Urbamais.Application.ViewModels.Response.v1.Role;
using Urbamais.Application.ViewModels.Response.v1.User;

namespace Urbamais.Application.Interfaces.Identity;

public interface IIdentityAppService
{
    //Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister);
    Task<Tuple<UserRegisterResponse, bool>> RegisterUser(UserRegisterRequest userRegister, string idUser);

    Task<UsuarioLoginResponse> Login(UserLoginRequest userLogin);

    Task<UsuarioLoginResponse> RefreshLogin(string userId);

    Task<RoleResponse> RegisterRole(RoleRequest roleRequest);

    Task<Tuple<bool, RoleResponse>> UpdateRole(string name, RoleRequest roleRequest);

    Task<Tuple<bool, RoleResponse>> DeleteRole(string name);
}