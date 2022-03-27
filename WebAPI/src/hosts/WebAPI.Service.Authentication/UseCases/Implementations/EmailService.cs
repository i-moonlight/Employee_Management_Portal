using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Serilog;
using WebAPI.Service.Authentication.UseCases.Options;
using WebAPI.Service.Authentication.UseCases.Services;

namespace WebAPI.Service.Authentication.UseCases.Implementations
{
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Send an email.
        /// </summary>
        /// <sem name="address">Mailbox to send.</param>
        /// <sem name="content">Email content.</param>
        /// <return>Successfully completed task.</returns>
        public Task SendEmail(string emailAddress, string content, EmailOptions options)
        {
            try
            {
                var mail = new MailMessage();
                var smtp = new SmtpClient(options.Host);

                mail.From = new MailAddress(options.Secret);
                mail.To.Add(emailAddress);
                mail.Subject = "Reset password";
                mail.Body = content;

                smtp.Port = options.Port;
                smtp.Credentials = new NetworkCredential(options.Secret, options.Key);
                smtp.EnableSsl = true;

                smtp.SendMailAsync(mail);
                Log.Information("The message was sent successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }

            return Task.CompletedTask;
        }
    }
}