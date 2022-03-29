using System.Threading.Tasks;
using WebAPI.Service.Authentication.UseCases.Options;

namespace WebAPI.Service.Authentication.UseCases.Services
{
    public interface IEmailService
    {
        Task SendEmail(string emailAddress, string content, EmailOptions options);
    }
}