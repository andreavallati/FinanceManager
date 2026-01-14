using FinanceManager.Shared.Enums;

namespace FinanceManager.Shared.Application.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public long UserId { get; set; } = long.MinValue;
        public string Username { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
