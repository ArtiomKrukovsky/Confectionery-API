using Confectionery.API.Application.Constants;
using Confectionery.API.Application.Services.Interfaces;
using Confectionery.API.Options;
using Confectionery.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Confectionery.API.Application.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        public readonly JwtOptions _jwtOptions;

        public JwtTokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        public string GenerateAccessToken(Client client)
        {
            var claims = CreateClaims(client);

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_jwtOptions.TokenLifeTime)),
                signingCredentials: new SigningCredentials(secret, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var randonNumberGenerator = RandomNumberGenerator.Create();

            randonNumberGenerator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token is invalid");
            }

            return principal;
        }

        private Claim[] CreateClaims(Client client)
        {
            // todo: do we need roles???

            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _jwtOptions.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtClaimNames.UserIdClaimName, client.Id.ToString()),
                new Claim(JwtClaimNames.UserNameClaimName, client.FullName),
            };
        }
    }
}
