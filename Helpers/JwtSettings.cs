// Helpers/JwtSettings.cs
namespace FoodSystem.Helpers
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationInDays { get; set; }
    }
}