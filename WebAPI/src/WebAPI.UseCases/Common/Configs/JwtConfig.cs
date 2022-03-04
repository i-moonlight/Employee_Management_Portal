using Microsoft.Extensions.Options;

namespace WebAPI.UseCases.Common.Configs
{
    public class JwtConfig : IOptions<JwtConfig>
    {
        public JwtConfig Value { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenLifeTime { get; set; }
    }
}