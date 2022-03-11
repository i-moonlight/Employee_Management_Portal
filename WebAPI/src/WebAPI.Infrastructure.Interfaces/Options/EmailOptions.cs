namespace WebAPI.Infrastructure.Interfaces.Options
{
    public class EmailOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Secret { get; set; }
        public string Key { get; set; }
    }
}