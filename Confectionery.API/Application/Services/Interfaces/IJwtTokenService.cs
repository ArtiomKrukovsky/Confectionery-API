using Confectionery.Domain.Entities;
using System.Security.Claims;

namespace Confectionery.API.Application.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User client);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
