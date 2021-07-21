using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.ViewModels;

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

        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
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
    }
}