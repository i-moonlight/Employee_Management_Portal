using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.ViewModels.DTO;
using WebAPI.Authentication.ViewModels.Request;
using WebAPI.Authentication.ViewModels.Response;

namespace WebAPI.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signManager)
        {
            _userManager = userManager;
            _signInManager = signManager;
        }

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
                var allUsersDto = new List<UserDto>();
                var users = _userManager.Users.ToList();

                foreach (var user in users)
                {
                    var roles = (await _userManager.GetRolesAsync(user)).ToList();

                    allUsersDto.Add(
                        new UserDto(user.FullName, user.Email, user.UserName, user.DateCreated, roles));
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

                    if (!result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(viewModel.Email);
                        var roles = (await _userManager.GetRolesAsync(appUser)).ToList();
                        var user = new UserDto(
                            appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated, roles);
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
    }
}
