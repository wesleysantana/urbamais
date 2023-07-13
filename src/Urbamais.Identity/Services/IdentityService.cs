using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Urbamais.Application.Interfaces.Identity;
using Urbamais.Application.ViewModels.Request.v1.User;
using Urbamais.Application.ViewModels.Response.v1.User;

namespace Urbamais.Identity.Services;

public class IdentityService : IIdentityAppService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(SignInManager<IdentityUser> signInManager,
                           UserManager<IdentityUser> userManager,
                           IOptions<JwtOptions> jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<UserRegisterResponse> RegisterUser(UserRegisterRequest usuarioCadastro)
    {
        var identityUser = new IdentityUser
        {
            UserName = usuarioCadastro.Email,
            Email = usuarioCadastro.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(identityUser, usuarioCadastro.Password);
        if (result.Succeeded)
            await _userManager.SetLockoutEnabledAsync(identityUser, false);

        var userRegisterResponse = new UserRegisterResponse(result.Succeeded);
        if (!result.Succeeded && result.Errors.Any())
            userRegisterResponse.AddErrors(result.Errors.Select(r => r.Description));

        return userRegisterResponse;
    }

    public async Task<UsuarioLoginResponse> Login(UserLoginRequest usuarioLogin)
    {
        var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);
        if (result.Succeeded)
            return await GerarCredenciais(usuarioLogin.Email);

        var userLoginResponse = new UsuarioLoginResponse();
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

    public async Task<UsuarioLoginResponse> RefreshLogin(string userId)
    {
        var userLoginResponse = new UsuarioLoginResponse();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            userLoginResponse.AddError("Usuário não localizado na nossa base de dados");

        if (await _userManager.IsLockedOutAsync(user!))
            userLoginResponse.AddError("Essa conta está bloqueada");
        else if (!await _userManager.IsEmailConfirmedAsync(user!))
            userLoginResponse.AddError("Essa conta precisa confirmar seu e-mail antes de realizar o login");

        if (userLoginResponse.Success)
            return await GerarCredenciais(user!.Email!);

        return userLoginResponse;
    }

    private async Task<UsuarioLoginResponse> GerarCredenciais(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var accessTokenClaims = await ObterClaims(user!, adicionarClaimsUsuario: true);
        var refreshTokenClaims = await ObterClaims(user!, adicionarClaimsUsuario: false);

        var dataExpiracaoAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
        var dataExpiracaoRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

        var accessToken = GerarToken(accessTokenClaims, dataExpiracaoAccessToken);
        var refreshToken = GerarToken(refreshTokenClaims, dataExpiracaoRefreshToken);

        return new UsuarioLoginResponse
        (
            accessToken: accessToken,
            refreshToken: refreshToken
        );
    }

    private string GerarToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
    {
        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: dataExpiracao,
            signingCredentials: _jwtOptions.SigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private async Task<IList<Claim>> ObterClaims(IdentityUser user, bool adicionarClaimsUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
        };

        if (adicionarClaimsUsuario)
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