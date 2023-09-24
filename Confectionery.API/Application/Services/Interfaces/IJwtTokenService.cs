using Confectionery.Domain.Entities;
using System.Security.Claims;

namespace Confectionery.API.Application.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
