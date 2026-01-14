namespace FinanceManager.Shared.Application.Configuration
{
    public class JwtSettings
    {
        public string ClientSecret { get; set; } = string.Empty;
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
    }
}