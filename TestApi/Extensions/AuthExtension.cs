
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TestApi.Models;

namespace TestApi.Extensions;

public static class AuthExtension 
{
    public static IServiceCollection AddAsymmetricAuthentication(IServiceCollection services) 
    {
       
        var issuerSigningKey = AuthOptions.GetSymmetricSecurityKey();
        services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidIssuer = AuthOptions.issuer,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigningKey,
                    LifetimeValidator = LifetimeValidator,
                    
                };
            });

        return services;
    }
    private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires,
        SecurityToken securityToken, TokenValidationParameters validationParameters) =>
        expires != null && expires > DateTime.UtcNow;
}