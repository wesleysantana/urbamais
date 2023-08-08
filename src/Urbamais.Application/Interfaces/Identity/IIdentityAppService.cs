using Urbamais.Application.ViewModels.Request.V1.Perfil;
using Urbamais.Application.ViewModels.Request.V1.User;
using Urbamais.Application.ViewModels.Response.V1.Perfil;
using Urbamais.Application.ViewModels.Response.V1.User;

namespace Urbamais.Application.Interfaces.Identity;

public interface IIdentityAppService
{    
    Task<Tuple<bool, UserRegisterResponse>> RegisterUser(UserRegisterRequest userRegister, string idUser);

    Task<UserLoginResponse> Login(UserLoginRequest userLogin);

    Task<UserLoginResponse> RefreshLogin(string userId);

    Task<List<UserResponse>> GetUsers(CancellationToken cancellationToken);

    Task<Tuple<bool, UserRegisterResponse>> UpdateUser(string userIdUpdate, UserUpdateRequest userRequest, string userId);

    Task<PerfilResponse> RegisterRole(PerfilRequest roleRequest);

    Task<Tuple<bool, PerfilResponse>> UpdateRole(string name, PerfilUpdateRequest roleRequest);

    Task<Tuple<bool, PerfilResponse>> DeleteRole(string name);

    Task<Tuple<bool, List<UserResponse>>> GetUsersInRole(string name);

    Task<Tuple<bool, UserRegisterResponse>> DeleteUser(string userIdDelete, string userId);
}