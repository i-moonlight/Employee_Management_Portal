using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Entities.Models;
using WebAPI.UseCases.Common.Configs;
using WebAPI.UseCases.Common.Dto;
using WebAPI.UseCases.Common.Dto.Request;
using WebAPI.UseCases.Common.Response;
using static WebAPI.UseCases.Common.Response.ResponseCode;
using static WebAPI.Utils.Constants.MessageTypes;
using static Microsoft.IdentityModel.Tokens.SecurityAlgorithms;

namespace WebAPI.UseCases.Requests.Authentication.Commands
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
        private readonly JwtConfig _jwtConfig;

        public SignInCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager,
            IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtConfig = jwtConfig.Value;
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

                if (result.IsNotAllowed)
                {
                    var appUser = await _userManager.FindByEmailAsync(request.LoginDto.Email);
                    var role = await _userManager.GetRolesAsync(appUser);

                    var user = new ProfileDto(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated,
                        role.ElementAt(0));

                    // Generate JWT.
                    user.Token = await GenerateToken(appUser);

                    return await Task.FromResult(new ResponseModel(Ok, TokenGenerated, user));
                }

                return await Task.FromResult(new ResponseModel(Error, InvalidEmailOrPassword, null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(Error, ex.Message, UserNotExist));
            }
        }

        /// <summary>
        /// Generate token.  
        /// </summary>
        /// <param name="user">User entity.</param>
        /// <returns>Return token.</returns>
        public async Task<string> GenerateToken(User user)
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
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}