namespace CalorieCounter.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public double ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
        public bool ValidateLifetime { get; set; }
    }
}