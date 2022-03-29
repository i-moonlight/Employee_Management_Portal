using Microsoft.Extensions.Options;

namespace WebAPI.Service.Authentication.UseCases.Options
{
    public class EmailOptions : IOptions<EmailOptions>
    {
        public EmailOptions Value { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Secret { get; set; }
        public string Key { get; set; }
    }
}