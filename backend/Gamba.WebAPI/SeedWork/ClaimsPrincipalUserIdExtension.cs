using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;

namespace Gamba.WebAPI.SeedWork;

public static class ClaimsPrincipalUserIdExtension
{
    public static Guid GetIdFromClaim(this ClaimsPrincipal user)
    {
        var idString = user.FindFirstValue(JwtRegisteredClaimNames.Jti) ??
                       throw new AuthenticationException("Id claim is not present");
        return Guid.Parse(idString);
    }
}