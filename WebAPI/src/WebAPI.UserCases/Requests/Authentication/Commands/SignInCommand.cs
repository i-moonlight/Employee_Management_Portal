using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Dto.Request;
using WebAPI.UserCases.Common.Response;
using static WebAPI.UserCases.Common.Response.ResponseCode;
using static WebAPI.Utils.Constants.MessageTypes;

namespace WebAPI.UserCases.Requests.Authentication.Commands
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class SignInCommand : IRequest<ResponseModel>
    {
        public LoginDto LoginDto { get; set; }
    }

    /// <summary>
    /// Implements a handler for the user sign in.
    /// </summary>
    public class SignInCommandHandler : IRequestHandler<SignInCommand, ResponseModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public SignInCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns ResponseModel.</returns>
        public async Task<ResponseModel> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(request.LoginDto.Username,
                    request.LoginDto.Password, true, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.FindByEmailAsync(request.LoginDto.Email);
                    var role = await _userManager.GetRolesAsync(appUser);

                    var user = new ProfileDto(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated,
                        role.ElementAt(0));

                    // user.Token = await GenerateToken(appUser);

                    user.Token = await _userManager.GenerateUserTokenAsync(new User(), "tokenProvider", "purpose");

                    return await Task.FromResult(new ResponseModel(Ok, TokenGenerated, user));
                }

                return await Task.FromResult(new ResponseModel(Error, InvalidEmailOrPassword, null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(Error, ex.Message, UserNotExist));
            }
        }
    }
}