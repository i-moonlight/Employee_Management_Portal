using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using WebAPI.Service.Authentication.Entities;
using WebAPI.Service.Authentication.UseCases.Dto;
using WebAPI.Service.Authentication.UseCases.Options;
using WebAPI.Service.Authentication.UseCases.Services;

namespace WebAPI.Service.Authentication.UseCases.Commands
{
    /// <summary>
    /// Sets the object as a property of the request command.
    /// </summary>
    public class ForgotPasswordCommand : IRequest<ResponseModel>
    {
        public AccountDto AccountDto { get; set; }
    }

    /// <summary>
    /// Implements a handler for reset forgot password.
    /// </summary>
    public class ForgotPasswordCommandHandler : Controller, IRequestHandler<ForgotPasswordCommand, ResponseModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IOptions<EmailOptions> _emailOptions;
        private readonly IHttpContextAccessor _contextAccessor;

        public ForgotPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService,
            IHttpContextAccessor contextAccessor, IOptions<EmailOptions> emailOptions)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
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
                var currentUser = await _userManager.FindByEmailAsync(request.AccountDto.Email);
                // _contextAccessor.HttpContext.Session.Set(SessionExtensions.UserKey, currentUser);
                //var cookies = _httpService.CheckCookies();
                // if (cookies != null)
                // {
                //     Response.Cookies.Append("User", currentUser);
                // }

                if (currentUser != null || ((User)null)!.EmailConfirmed)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);
                    var header = _contextAccessor.HttpContext!.Request.Headers["ChangePasswordUrl"];

                    // Generate email link as string -> unreachable test mock.
                    var emailLink = await GenerateEmailLink(request, token, header, currentUser);

                    // Send email service result -> unreachable test mock.
                    await _emailService.SendEmail(request.AccountDto.Email, emailLink, _emailOptions.Value);

                    return await Task.FromResult(new ResponseModel(ResponseCode.Ok, true,
                        "The link to collect the password was sent to the mail", ""));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, false,
                    "Failed to send a message to this address", ""));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, false, ex.Message, ""));
            }
        }

        /// <summary>
        /// Generate email link.  
        /// </summary>
        /// <param name="token">Access token.</param>
        /// <param name="changePasswordUrl">StringValues.</param>
        /// <param name="user">User entity.</param>
        /// <returns>Return token.</returns>
        public Task<string> GenerateEmailLink(ForgotPasswordCommand request, string token, StringValues header,
            User user)
        {
            var uriBuilder = new UriBuilder(header);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["userid"] = user.Id;
            uriBuilder.Query = request.AccountDto.ResetPasswordUri!;

            var recoveryLink = uriBuilder.ToString();
            var emailBody = $"Click on link to change password {recoveryLink}";
            return Task.FromResult(emailBody);
        }
    }
}