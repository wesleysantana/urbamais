using Urbamais.Application.ViewModels.Request.V1.Role;
using Urbamais.Application.ViewModels.Request.V1.User;
using Urbamais.Application.ViewModels.Response.V1.Role;
using Urbamais.Application.ViewModels.Response.V1.User;

namespace Urbamais.Application.Interfaces.Identity;

public interface IIdentityAppService
{    
    Task<Tuple<bool, UserRegisterResponse>> RegisterUser(UserRegisterRequest userRegister, string idUser);

    Task<UserLoginResponse> Login(UserLoginRequest userLogin);

    Task<UserLoginResponse> RefreshLogin(string userId);

    Task<List<UserResponse>> GetUsers(CancellationToken cancellationToken);

    Task<Tuple<bool, UserRegisterResponse>> UpdateUser(string userIdUpdate, UserUpdateRequest userRequest, string userId);

    Task<RoleResponse> RegisterRole(RoleRequest roleRequest);

    Task<Tuple<bool, RoleResponse>> UpdateRole(string name, RoleUpdateRequest roleRequest);

    Task<Tuple<bool, RoleResponse>> DeleteRole(string name);

    Task<Tuple<bool, List<UserResponse>>> GetUsersInRole(string name);

    Task<Tuple<bool, UserRegisterResponse>> DeleteUser(string userIdDelete, string userId);
}