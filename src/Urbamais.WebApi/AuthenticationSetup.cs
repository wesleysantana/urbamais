﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Urbamais.Identity;

namespace Urbamais.WebApi;

internal static class AuthenticationSetup
{
    internal static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtAppSettingOptions = configuration.GetSection(nameof(JwtOptions));
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecurityKey").Value!));

        services.Configure<JwtOptions>(options =>
        {
            options.Issuer = jwtAppSettingOptions[nameof(JwtOptions.Issuer)]!;
            options.Audience = jwtAppSettingOptions[nameof(JwtOptions.Audience)]!;
            options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            options.AccessTokenExpiration = int.Parse(jwtAppSettingOptions[nameof(JwtOptions.AccessTokenExpiration)] ?? "0");
            options.RefreshTokenExpiration = int.Parse(jwtAppSettingOptions[nameof(JwtOptions.RefreshTokenExpiration)] ?? "0");
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
        });

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

            ValidateAudience = true,
            ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = tokenValidationParameters;
        });        
    }

    //public static void AddAuthorizationPolicies(this IServiceCollection services)
    //{
    //    services.AddSingleton<IAuthorizationHandler, HorarioComercialHandler>();
    //    services.AddAuthorization(options =>
    //    {
    //        options.AddPolicy(Policies.HorarioComercial, policy =>
    //            policy.Requirements.Add(new HorarioComercialRequirement()));
    //    });
    //}
}
