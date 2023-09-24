namespace Confectionery.API.Options
{
    public class JwtOptions
    {
        public const string Jwt = "Jwt";

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public string Secret { get; set; }
        public string TokenLifeTime { get; set; }
        public string RefreshTokenLifetime { get; set; }
    }
}
