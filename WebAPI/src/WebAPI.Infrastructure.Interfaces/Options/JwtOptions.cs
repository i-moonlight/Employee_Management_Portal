using Microsoft.Extensions.Options;

namespace WebAPI.Infrastructure.Interfaces.Options
{
    public class JwtOptions : IOptions<JwtOptions>
    {
        public JwtOptions Value { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenLifeTime { get; set; }
    }
}