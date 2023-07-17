using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Urbamais.Application.Interfaces.Identity;
using Urbamais.Application.ViewModels.Request.v1.Role;
using Urbamais.Application.ViewModels.Request.v1.User;
using Urbamais.Application.ViewModels.Response.v1.Role;
using Urbamais.Application.ViewModels.Response.v1.User;

namespace Urbamais.Identity.Services;

public class IdentityService : IIdentityAppService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(SignInManager<ApplicationUser> signInManager,
                           UserManager<ApplicationUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IOptions<JwtOptions> jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtOptions = jwtOptions.Value;
    }

    //public async Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister)
    //{
    //    var identityUser = new IdentityUser
    //    {
    //        UserName = userRegister.Email,
    //        Email = userRegister.Email,
    //        EmailConfirmed = true
    //    };

    //    var result = await _userManager.CreateAsync(identityUser, userRegister.Password);
    //    if (result.Succeeded)
    //        await _userManager.SetLockoutEnabledAsync(identityUser, false);

    //    var userRegisterResponse = new UserRegisterResponse(result.Succeeded);
    //    if (!result.Succeeded && result.Errors.Any())
    //        userRegisterResponse.AddErrors(result.Errors.Select(r => r.Description));

    //    return userRegisterResponse;
    //}

    public async Task<Tuple<UserRegisterResponse, bool>> RegisterUser(UserRegisterRequest userRegister, string idUser)
    {
        var role = _roleManager.FindByNameAsync(userRegister.Role);

        if (role.Result is null)
        {
            var response = new UserRegisterResponse();
            response.AddErrors(new List<string> { "Perfil de acesso não localizado." });
            return Tuple.Create(response, false);
        }

        var identityUser = new ApplicationUser
        {
            UserName = userRegister.Email,
            Email = userRegister.Email,
            EmailConfirmed = true,
            IdUserCreation = idUser,
            CreationDate = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(identityUser, userRegister.Password);
        if (result.Succeeded)
        {
            await _userManager.SetLockoutEnabledAsync(identityUser, false);
            await _userManager.AddToRoleAsync(identityUser, userRegister.Role);
        }

        var userRegisterResponse = new UserRegisterResponse(result.Succeeded);
        if (!result.Succeeded && result.Errors.Any())
            userRegisterResponse.AddErrors(result.Errors.Select(r => r.Description));

        return Tuple.Create(userRegisterResponse, true);
    }

    public async Task<List<UserResponse>> GetUsers(CancellationToken cancellationToken)
    {
        var result = await _userManager.Users.Where(x => x.DeletionDate == null).ToListAsync(cancellationToken);        

        var listResponse = new List<UserResponse>();

        foreach (var item in result)
        {
            listResponse.Add(new UserResponse
            {
                Email = item.Email!,
                Name = item.Name
            });
        }

        return listResponse;
    }
    

    public async Task<UserLoginResponse> Login(UserLoginRequest userLogin)
    {
        var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);
        if (result.Succeeded)
            return await CredentialRegister(userLogin.Email);

        var userLoginResponse = new UserLoginResponse();
        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                userLoginResponse.AddError("Essa conta está bloqueada");
            else if (result.IsNotAllowed)
                userLoginResponse.AddError("Essa conta não tem permissão para fazer login");
            else if (result.RequiresTwoFactor)
                userLoginResponse.AddError("É necessário confirmar o login no seu segundo fator de autenticação");
            else
                userLoginResponse.AddError("Usuário ou senha estão incorretos");
        }

        return userLoginResponse;
    }

    public async Task<UserLoginResponse> RefreshLogin(string userId)
    {
        var userLoginResponse = new UserLoginResponse();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            userLoginResponse.AddError("Usuário não localizado na nossa base de dados");

        if (await _userManager.IsLockedOutAsync(user!))
            userLoginResponse.AddError("Essa conta está bloqueada");
        else if (!await _userManager.IsEmailConfirmedAsync(user!))
            userLoginResponse.AddError("Essa conta precisa confirmar seu e-mail antes de realizar o login");

        if (userLoginResponse.Success)
            return await CredentialRegister(user!.Email!);

        return userLoginResponse;
    }

    public async Task<Tuple<bool, UserRegisterResponse>> DeleteUser(string userIdDelete, string userId)
    {
        var user = await _userManager.FindByIdAsync(userIdDelete.ToString());

        if (user is null)
            return Tuple.Create(false, new UserRegisterResponse());

        user.Delete(userId);
        var result = await _userManager.UpdateAsync(user);

        var roleRegisterResponse = new UserRegisterResponse(result.Succeeded);

        if (!result.Succeeded && result.Errors.Any())
            roleRegisterResponse.AddErrors(result.Errors.Select(r => r.Description));

        return Tuple.Create(true, roleRegisterResponse);
    }

    public async Task<RoleResponse> RegisterRole(RoleRequest roleRequest)
    {
        var identityRole = new IdentityRole
        {
            Name = roleRequest.Name
        };

        var result = await _roleManager.CreateAsync(identityRole);

        var roleRegisterResponse = new RoleResponse(result.Succeeded);
        if (!result.Succeeded && result.Errors.Any())
            roleRegisterResponse.AddErrors(result.Errors.Select(r => r.Description));

        return roleRegisterResponse;
    }

    public async Task<Tuple<bool, RoleResponse>> UpdateRole(string name, RoleRequest roleRequest)
    {
        var role = _roleManager.FindByNameAsync(name).Result;

        if (role is null)
            return Tuple.Create(false, new RoleResponse());

        role.Name = roleRequest.Name;

        var result = await _roleManager.UpdateAsync(role);

        var roleRegisterResponse = new RoleResponse(result.Succeeded);
        if (!result.Succeeded && result.Errors.Any())
            roleRegisterResponse.AddErrors(result.Errors.Select(r => r.Description));

        return Tuple.Create(true, roleRegisterResponse);
    }

    public async Task<Tuple<bool, RoleResponse>> DeleteRole(string name)
    {
        var role = _roleManager.FindByNameAsync(name).Result;

        if (role is null)
            return Tuple.Create(false, new RoleResponse());

        var usersInRole = await _userManager.GetUsersInRoleAsync(name);

        if (usersInRole.Any(x => x.DeletionDate is null))
        {
            var roleResponse = new RoleResponse(false);
            roleResponse.AddErrors(new List<string> { "Existem usuário(s) ativo(s) com esta permissão. Exclusão não permitida" });
            return Tuple.Create(true, roleResponse);
        }

        if (usersInRole.Any())
        {
            // Excluir manualmente as associações de usuários com a função
            foreach (var user in usersInRole)
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name!);
            }
        }

        var result = await _roleManager.DeleteAsync(role);

        var roleRegisterResponse = new RoleResponse(result.Succeeded);

        if (!result.Succeeded && result.Errors.Any())
            roleRegisterResponse.AddErrors(result.Errors.Select(r => r.Description));

        return Tuple.Create(true, roleRegisterResponse);
    }

    public async Task<Tuple<bool, List<UserResponse>>> GetUsersInRole(string name)
    {
        var role = _roleManager.FindByNameAsync(name).Result;

        if (role is null)
            return Tuple.Create(false, new List<UserResponse>());

        var usersInRole = await _userManager.GetUsersInRoleAsync(name);
        
        var listReturn = new List<UserResponse>();
        foreach (var item in usersInRole)
        {
            listReturn.Add(new UserResponse
            {
                Name = item.Name,
                Email = item.Email!
            });
        }

        return Tuple.Create(true, listReturn);
    }

    private async Task<UserLoginResponse> CredentialRegister(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var accessTokenClaims = await GetClaims(user!, addClaimsUser: true);
        var refreshTokenClaims = await GetClaims(user!, addClaimsUser: false);

        var dataExpiracaoAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
        var dataExpiracaoRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

        var accessToken = TokenRegister(accessTokenClaims, dataExpiracaoAccessToken);
        var refreshToken = TokenRegister(refreshTokenClaims, dataExpiracaoRefreshToken);

        return new UserLoginResponse
        (
            accessToken: accessToken,
            refreshToken: refreshToken
        );
    }

    private string TokenRegister(IEnumerable<Claim> claims, DateTime DateExpiration)
    {
        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateExpiration,
            signingCredentials: _jwtOptions.SigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private async Task<IList<Claim>> GetClaims(ApplicationUser user, bool addClaimsUser)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
        };

        if (addClaimsUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(userClaims);

            foreach (var role in roles)
                claims.Add(new Claim("role", role));
        }

        return claims;
    }   
}