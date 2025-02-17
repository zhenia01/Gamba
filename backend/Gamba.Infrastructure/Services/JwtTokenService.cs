using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gamba.Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace Gamba.Infrastructure.Services;

public class JwtTokenService
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly SymmetricSecurityKey _securityKey;

    public JwtTokenService(string issuer, string audience, SymmetricSecurityKey securityKey)
    {
        _issuer = issuer;
        _audience = audience;
        _securityKey = securityKey;
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Name),
            new(JwtRegisteredClaimNames.Jti, user.Id.Value.ToString()),
            new(ClaimTypes.Name, user.Name),
        };

        if (user.IsCreator)
        {
            claims.Add(new(ClaimTypes.Role, UserRoles.Creator));
        }

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            signingCredentials: signingCredentials
        );

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodedToken;
    }
}