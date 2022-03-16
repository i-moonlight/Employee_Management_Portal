using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.UseCases.Common.Dto.Auth;
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
        // private readonly UserManager<User> _userManager;
        // private readonly IOptions<EmailOptions> _emailOptions;
        // private readonly IEmailService _emailService;
        //
        // public AuthController(UserManager<User> userManager, IOptions<EmailOptions> emailOptions,
        //     IEmailService emailService)
        // {
        //     _emailOptions = emailOptions;
        //     _userManager = userManager;
        //     _emailService = emailService;
        // }

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
            var request = new RegisterUserCommand() { RegisterUserDto = registerUser };
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
            var request = new SignInCommand() { LoginDto = login };
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Forgot user password.  
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST -> /auth/ForgotPassword.
        /// </remarks>
        /// <param name="dto">Data from app client.</param>
        /// <returns>Response model.</returns>
        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<ResponseModel>> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var request = new ForgotPasswordCommand() { ForgotPasswordDto = dto };
            return Ok(await Mediator.Send(request));
        }
    }
}