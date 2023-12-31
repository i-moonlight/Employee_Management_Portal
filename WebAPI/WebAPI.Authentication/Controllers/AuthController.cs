﻿using System;
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
using WebAPI.Authentication.Data.Entities;
using WebAPI.Authentication.Infrastructure;
using WebAPI.Authentication.Models;

namespace WebAPI.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtConfig _jwtConfig;

        public AuthController(UserManager<User> manager, SignInManager<User> signManager, IOptions<JwtConfig> jwtConfig)
        {
            _userManager = manager;
            _signInManager = signManager;
            _jwtConfig = jwtConfig.Value;
        }
        
        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] RegistrationModel viewModel)
        {
            try
            {
                var user = new User()
                {
                    FullName = viewModel.FullName,
                    Email = viewModel.Email,
                    EmailConfirmed = true,
                    UserName = viewModel.Username,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    var tempUser = await _userManager.FindByEmailAsync(viewModel.Email);
                    // foreach (var role in RoleNames.AllRoles)
                    // {
                    //     await _userManager.AddToRoleAsync(tempUser, role);
                    // }

                    await _userManager.AddToRoleAsync(tempUser, RoleNames.AllRoles.ElementAt(0));

                    return await Task.FromResult(new Response(
                        ResponseCode.Ok, "User has been Registered", null)
                    );
                }

                return await Task.FromResult(new Response(
                    ResponseCode.Ok,
                    "User has been not Registered",
                    result.Errors.Select(x => x.Description).ToArray())
                );
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response(ResponseCode.Error, ex.Message, null));
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
                var profiles = new List<Profile>();
                var users = _userManager.Users.ToArray();

                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).ToList();

                    profiles.Add(new Profile(user.FullName, user.Email, user.UserName, user.DateCreated,
                        string.Join(" ", role)));
                }

                return await Task.FromResult(new Response(ResponseCode.Ok, "", profiles));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response(ResponseCode.Error, ex.Message, null));
            }
        }

        /// <summary>
        /// Validate login into App.  
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Response model</returns>
        // [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, true, false);

                    if (result != null)
                    {
                        var appUser = await _userManager.FindByEmailAsync(login.Email);
                        var role = (await _userManager.GetRolesAsync(appUser)).ToString();
                        var user = new Profile(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated, role);
                        user.Token = GenerateToken(appUser);
                        return await Task.FromResult(new Response(ResponseCode.Ok, "", user));
                    }
                }

                return await Task.FromResult(new Response(ResponseCode.Error, "invalid Email or password", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response(ResponseCode.Error, ex.Message, null));
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
