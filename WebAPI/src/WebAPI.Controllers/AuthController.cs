using System;
using System.Collections;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.Options;
using WebAPI.Infrastructure.Interfaces.Services;
using WebAPI.UseCases.Common.Dto;
using WebAPI.UseCases.Common.Dto.Auth;
using WebAPI.UseCases.Common.Dto.Request;
using WebAPI.UseCases.Common.Dto.Response;
using WebAPI.UseCases.Requests.Authentication.Commands;
using WebAPI.UseCases.Requests.Authentication.Queries;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<EmailOptions> _emailOptions;
        private readonly IEmailService _emailService;

        public AuthController(UserManager<User> userManager, IOptions<EmailOptions> emailOptions,
            IEmailService emailService)
        {
            _emailOptions = emailOptions;
            _userManager = userManager;
            _emailService = emailService;
        }

        /// <summary>
        /// Get all user from database.   
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/GetAllUsers.
        /// <returns>User list.</returns>
        /// <response code="200">Success.</response>
        // [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable>> GetUserList()
        {
            var request = new GetUserListQuery();
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/RegisterUser.
        /// </remarks>
        /// <param name="registerUser">RegisterUserDto.</param>
        /// <returns>Response model.</returns>
        /// <response code="200">Success.</response>
        [HttpPost("RegisterUser")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> RegisterUser([FromBody] RegisterUserDto registerUser)
        {
            var request = new RegisterUserCommand() {RegisterUserDto = registerUser};
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Sign in App.  
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// POST /auth/SignIn.
        /// </remarks>
        /// <param name="login">LoginDto.</param>
        /// <returns>Response model.</returns>
        // [AllowAnonymous]
        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> SignIn([FromBody] LoginDto login)
        {
            var request = new SignInCommand() {LoginDto = login};
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Forgot user password.  
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST -> /auth/ForgotPassword.
        /// </remarks>
        /// <param name="dto"> Data from app client. </param>
        /// <returns> Response model. </returns>
        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<ResponseModel>> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user != null || user!.EmailConfirmed)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var changePasswordUrl = HttpContext.Request.Headers["changePasswordUrl"];

                    var uriBuilder = new UriBuilder(changePasswordUrl);
                    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                    query["token"] = token;
                    query["userid"] = user.Id;
                    uriBuilder.Query = query.ToString();
                    var urlString = uriBuilder.ToString();

                    var emailBody = $"Click on link to change password </br>{urlString}";

                    await _emailService.SendEmail(dto.Email, emailBody, _emailOptions.Value);

                    return await Task.FromResult(new ResponseModel(
                        ResponseCode.Ok, "The link to collect the password was sent to the mail", null));
                }

                return await Task.FromResult(new ResponseModel(
                    ResponseCode.Error, "Failed to send a message to this address", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                    new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }
    }
}