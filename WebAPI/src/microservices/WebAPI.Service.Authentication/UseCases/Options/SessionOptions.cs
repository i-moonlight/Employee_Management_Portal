using Microsoft.Extensions.Options;

namespace WebAPI.Service.Authentication.UseCases.Options
{
    public class SessionOptions : IOptions<SessionOptions>
    {
        public SessionOptions Value { get; set; }
        public string Cookie { get; set; }
        public string Set { get; set; }
    }
}