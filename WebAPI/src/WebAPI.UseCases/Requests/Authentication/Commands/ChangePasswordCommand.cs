using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAPI.Entities.Models;
using WebAPI.UseCases.Common.Dto.Auth;
using WebAPI.UseCases.Common.Dto.Response;

namespace WebAPI.UseCases.Requests.Authentication.Commands
{
    public class ChangePasswordCommand : IRequest<ResponseModel>
    {
        public AccountDto AccountDto { get; set; }
    }

    /// <summary>
    /// Implements a handler for change user password.
    /// </summary>
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ResponseModel>
    {
        private readonly UserManager<User> _userManager;

        public ChangePasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">ChangePasswordCommand.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<ResponseModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var c = ConfigurationConfiguration["SessionOptions:Cookie"];
                //var userid = _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                //_httpContextAccessor.HttpContext.Session.Get("UserKey");
                //var userid = _httpService.GetCookies("User");

                var email = request.AccountDto.Email;
                var currentUser = await _userManager.FindByEmailAsync(email);
                var token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);

                var resetPasswordResult = await _userManager.ResetPasswordAsync(currentUser,
                    Uri.UnescapeDataString(token), request.AccountDto.Password);

                if (resetPasswordResult.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Ok, true,
                        "Password changed successful", ""));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, false,
                    "Password changed failed", ""));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, false, ex.Message, ""));
            }
        }
    }
}