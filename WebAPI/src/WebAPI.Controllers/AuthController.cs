using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Entities.Common;
using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Request;
using WebAPI.UserCases.Common.Response;
using WebAPI.UserCases.Requests.Authentication.Commands;
using static System.Security.Claims.ClaimTypes;
using static WebAPI.UserCases.Common.Response.ResponseCode;

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
        private readonly SignInManager<User> _signInManager;
        private readonly JwtConfig _jwtConfig;

        public AuthController(UserManager<User> userManager, SignInManager<User> signManager,
            IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _jwtConfig = jwtConfig.Value;
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
        /// Get All User from database.   
        /// </summary>
        /// <returns>Response model.</returns>
        // [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<object> GetAllUsers()
        {
            try
            {
                var profilesDto = new List<ProfileDto>();
                var users = _userManager.Users.ToArray();

                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).ToList();

                    profilesDto.Add(
                        new ProfileDto(user.FullName, user.Email, user.UserName, user.DateCreated,
                            string.Join(" ", role)));
                }

                return await Task.FromResult(
                    new ResponseModel(ResponseCode.Ok, "", profilesDto));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                    new ResponseModel(Error, ex.Message, null));
            }
        }

        /// <summary>
        /// Sign in App.  
        /// </summary>
        /// <param name="login">LoginDto.</param>
        /// <returns>Response model.</returns>
        // [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<object> SignIn([FromBody] LoginDto login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);

                    if (!result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(login.Email);
                        var role = (await _userManager.GetRolesAsync(appUser)).ToList();

                        var user = new ProfileDto(
                            appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated,
                            role.ElementAt(0));

                        user.Token = await GenerateToken(appUser);

                        return await Task.FromResult(
                            new ResponseModel(ResponseCode.Ok, "Token generated", user));
                    }
                }

                return await Task.FromResult(
                    new ResponseModel(Error, "Invalid email or password", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                    new ResponseModel(Error, ex.Message, "The user does not exist"));
            }
        }

        /// <summary>
        /// Generate token.  
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>Token.</returns>
        private async Task<string> GenerateToken(User user)
        {
            var userId = await _userManager.FindByIdAsync(user.Id);
            var userRoles = await _userManager.GetRolesAsync(userId);

            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.NameId, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (userRoles != null)
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(Role, role));
                }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}