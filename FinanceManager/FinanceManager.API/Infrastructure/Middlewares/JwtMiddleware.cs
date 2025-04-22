using FinanceManager.Shared.Application.Configuration;
using FinanceManager.Shared.Constants;
using FinanceManager.Shared.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceManager.API.Infrastructure.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger, IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var endpoint = httpContext.Request.Path.Value;

            // Bypass token validation for /api/auth/login and /api/users/register
            if (endpoint is not null && IsTokenNotRequired(endpoint))
            {
                await _next(httpContext);
                return;
            }

            var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authHeader?.Split(' ').LastOrDefault();

            if (string.IsNullOrEmpty(token))
            {
                throw new AuthorizationException("Missing access token");
            }

            var claimPrincipal = ValidateToken(token);

            if (claimPrincipal is null)
            {
                throw new AuthorizationException("Invalid access token");
            }

            // Attach user to HttpContext
            httpContext.User = claimPrincipal;
            await _next(httpContext);
        }

        private static bool IsTokenNotRequired(string endpoint)
        {
            return endpoint.StartsWith(Common.LoginEndpoint, StringComparison.OrdinalIgnoreCase) ||
                   endpoint.StartsWith(Common.RegistrationEndpoint, StringComparison.OrdinalIgnoreCase);
        }

        private ClaimsPrincipal? ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Value.ClientSecret);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = _jwtSettings.Value.ValidateIssuer,
                    ValidateAudience = _jwtSettings.Value.ValidateAudience,
                    ValidateLifetime = _jwtSettings.Value.ValidateLifetime,
                    ValidateIssuerSigningKey = _jwtSettings.Value.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };

                var claimPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return claimPrincipal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Token validation failed: {{message}}", ex.Message);
                return null;
            }
        }
    }
}