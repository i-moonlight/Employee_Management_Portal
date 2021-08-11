using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication.Data.Entities;
using WebAPI.Authentication.Models;

namespace WebAPI.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IOptions<EmailOptionsDTO> _emailOptions;
        //private readonly IEmail _email;
        //private readonly ICloudStorage _cloudStorage;
        //private readonly IMapper _mapper;

        public RegistrationController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
            //IOptions<EmailOptionsDTO> emailOptions,
            //IEmail email,
            //ICloudStorage cloudStorage,
            //IMapper mapper
            )
        {
            //_mapper = mapper;
            //_email = email;
            //_emailOptions = emailOptions;
            //_cloudStorage = cloudStorage;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return new JsonResult("СТРАНИЦА ЗАГРУЖЕНА");
        }
        
        // Post api/employers/create
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegistrationEmployeeModel model)
        {
            if (!(await _roleManager.RoleExistsAsync("Employer")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Employer"));
            }

            var employer = new User
            {
                UserName = model.Username,
                Email = model.Email
            };
            
            var result = await _userManager.CreateAsync(employer, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            //Send Email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(employer);
            var confirmEmailUrl = Request.Headers["confirmEmailUrl"]; //http://localhost:4200/email-confirm
            var uriBuilder = new UriBuilder(confirmEmailUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["userid"] = employer.Id;
            uriBuilder.Query = query.ToString();
            
            //var urlString = uriBuilder.ToString();
            //var emailBody = $"Please confirm your email by clicking on the link below </br>{urlString}";
            //await _email.Send(model.Email, emailBody, _emailOptions.Value);
            
            var userFromDb = await _userManager.FindByNameAsync(employer.UserName);
            await _userManager.AddToRoleAsync(userFromDb, "Employer");

            return Ok(result);
        }
    }
}