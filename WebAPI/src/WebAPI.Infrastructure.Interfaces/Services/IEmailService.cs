using System.Threading.Tasks;
using WebAPI.Infrastructure.Interfaces.Options;

namespace WebAPI.Infrastructure.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmail(string emailAddress, string content, EmailOptions options);
    }
}