using FinanceManager.Shared.Enums;

namespace FinanceManager.Shared.Application.Authentication
{
    public static class SessionManager
    {
        public static string? Token { get; private set; }
        public static long UserId { get; private set; }
        public static string? Username { get; private set; }
        public static UserRole Role { get; private set; }

        public static void SetSession(string token, long id, string email, UserRole role)
        {
            Token = token;
            UserId = id;
            Username = email;
            Role = role;
        }

        public static void Clear()
        {
            Token = null;
            UserId = long.MinValue;
            Username = null;
            Role = UserRole.Standard;
        }

        public static bool IsAuthenticated => !string.IsNullOrEmpty(Token);
        public static bool IsAdmin => Role == UserRole.Admin;
        public static bool IsStandard => Role == UserRole.Standard;
    }
}