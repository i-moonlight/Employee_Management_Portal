using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.Options;
using WebAPI.Infrastructure.Interfaces.Services;
using WebAPI.UseCases.Common.Dto.Auth;
using WebAPI.UseCases.Common.Dto.Response;

namespace WebAPI.UseCases.Requests.Authentication.Commands
{
    /// <summary>
    /// Sets the object as a property of the request command.
    /// </summary>
    public class ForgotPasswordCommand : IRequest<ResponseModel>
    {
        public ForgotPasswordDto ForgotPasswordDto { get; set; }
    }

    /// <summary>
    /// Implements a handler for reset forgot password.
    /// </summary>
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ResponseModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<EmailOptions> _emailOptions;

        public ForgotPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService,
            IHttpContextAccessor httpContextAccessor, IOptions<EmailOptions> emailOptions)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _emailOptions = emailOptions;
        }

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">ForgotPasswordCommand.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<ResponseModel> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.ForgotPasswordDto.Email);

                if (user != null || ((User)null)!.EmailConfirmed)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var changePasswordUrl = _httpContextAccessor.HttpContext!.Request.Headers["changePasswordUrl"];
                    
                    // Generate email link as string.
                    var emailLink = await GenerateEmailLink(token, changePasswordUrl, user);
                    await _emailService.SendEmail(request.ForgotPasswordDto.Email, emailLink, _emailOptions.Value);

                    return await Task.FromResult(new ResponseModel(ResponseCode.Ok,
                        "The link to collect the password was sent to the mail", ""));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.Error,
                    "Failed to send a message to this address", ""));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, ""));
            }
        }

        /// <summary>
        /// Generate email link.  
        /// </summary>
        /// <param name="token">Access token.</param>
        /// <param name="changePasswordUrl">StringValues.</param>
        /// <param name="user">User entity.</param>
        /// <returns>Return token.</returns>
        public Task<string> GenerateEmailLink(string token, StringValues changePasswordUrl, User user)
        {
            var uriBuilder = new UriBuilder(changePasswordUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["userid"] = user.Id;
            uriBuilder.Query = query.ToString()!;

            var urlString = uriBuilder.ToString();
            var emailBody = $"Click on link to change password </br>{urlString}";
            return Task.FromResult(emailBody);
        }
    }
}