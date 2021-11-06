using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authentication.ViewModels;
using WebAPI.Authentication.ViewModels.DTO;
using WebAPI.Authentication.ViewModels.Request;
using WebAPI.Authentication.ViewModels.Response;
using WebAPI.Domain.Core.Entities;
using WebAPI.Infrastructure.Data.Identity;

namespace WebAPI.Authentication.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtConfig _jwtConfig;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signManager,
            IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _jwtConfig = jwtConfig.Value;
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Response model</returns>
        [HttpPost("RegisterUser")]
        public async Task<object> AddRegisterUser([FromBody] RegisterUserViewModel viewModel)
        {
            try
            {
                var user = new User()
                {
                    FullName = viewModel.FullName,
                    Email = viewModel.Email,
                    EmailConfirmed = true,
                    UserName = viewModel.UserName,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    var tempUser = await _userManager.FindByEmailAsync(viewModel.Email);
                    foreach (var role in RoleNames.AllRoles)
                    {
                        await _userManager.AddToRoleAsync(tempUser, role);
                    }

                    return await Task.FromResult(
                        new ResponseModel(ResponseCode.Ok, "User has been Registered", null));
                }

                return await Task.FromResult(
                    new ResponseModel(ResponseCode.Error, "",
                        result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                    new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }
        
        /// <summary>
        /// Get All User from database.   
        /// </summary>
        /// <returns>Response model</returns>
        // [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<object> GetAllUsers()
        {
            try
            {
                var allUsersDto = new List<Account>();
                var users = _userManager.Users.ToList();

                foreach (var user in users)
                {
                    var roles = (await _userManager.GetRolesAsync(user)).ToList();

                    allUsersDto.Add(
                        new Account(user.FullName, user.Email, user.UserName, user.DateCreated, roles));
                }

                return await Task.FromResult(
                    new ResponseModel(ResponseCode.Ok, "", allUsersDto));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                    new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }
        
        /// <summary>
        /// Validate login into App.  
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Response model</returns>
        // [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager
                        .PasswordSignInAsync(viewModel.Email, viewModel.Password, true, false);

                    if (result != null)
                    {
                        var appUser = await _userManager.FindByEmailAsync(viewModel.Email);
                        var role = (await _userManager.GetRolesAsync(appUser)).ToList();
                        var user = new Account(
                            appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated, role);
                        user.Token = GenerateToken(appUser);
                        return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "", user));
                    }
                }

                return await Task.FromResult(
                    new ResponseModel(ResponseCode.Error, "invalid Email or password", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                    new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }
        
        /// <summary>
        /// Generate token.  
        /// </summary>
        /// <param name="user"></param>
        /// <returns>token</returns>
        private string GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // foreach (var role in user.Roles)
            // {
            //     claims.Add(new Claim("role", role.ToString()));
            // }

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