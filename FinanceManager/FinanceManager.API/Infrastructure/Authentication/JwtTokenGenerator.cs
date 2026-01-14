using FinanceManager.API.Application.Interfaces.Authentication;
using FinanceManager.Shared.Application.Configuration;
using FinanceManager.Shared.Application.Dtos;
using FinanceManager.Shared.Application.Responses;
using FinanceManager.Shared.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceManager.API.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly string _clientSecret;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _clientSecret = jwtSettings.Value.ClientSecret;
        }

        public TokenResponse GenerateToken(UserDto user)
        {
            if (user is null)
            {
                throw new TechnicalException("User data not provided");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_clientSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: credentials);

            var tokenResponse = new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                Username = user.Email ?? string.Empty,
                Role = user.Role
            };

            return tokenResponse;
        }
    }
}