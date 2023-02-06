using System.Text;
using Gamba.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Gamba.WebAPI.Configuration;

public static class JwtConfiguration
{
    public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("JwtIssuerOptions");

        var secretKey = jwtOptions["Key"]!;
        var issuer = jwtOptions["Issuer"]!;
        var audience = jwtOptions["Audience"]!;
        
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey!));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = securityKey,
                    
                    ClockSkew = TimeSpan.Zero,
                    
                    RequireExpirationTime = false 
                };
            });

        services.AddSingleton(new JwtTokenService(issuer, audience, securityKey));
    }

    public static void UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}