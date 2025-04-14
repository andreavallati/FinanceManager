namespace FinanceManager.Shared.Application.Authentication
{
    public static class SessionManager
    {
        public static string? Token { get; private set; }
        public static string? Email { get; private set; }

        public static void SetSession(string token, string email)
        {
            Token = token;
            Email = email;
        }

        public static void Clear()
        {
            Token = null;
            Email = null;
        }

        public static bool IsAuthenticated => !string.IsNullOrEmpty(Token);
    }
}