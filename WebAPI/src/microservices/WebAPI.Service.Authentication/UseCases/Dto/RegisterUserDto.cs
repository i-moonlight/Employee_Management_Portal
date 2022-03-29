using WebAPI.Service.Authentication.Entities;
using WebAPI.Service.Authentication.UseCases.Common.Mapping;

namespace WebAPI.Service.Authentication.UseCases.Dto
{
    /// <summary>
    /// Sets a properties of the data transfer object for user entity. : IMapFrom<User>
    /// </summary>
    public class RegisterUserDto : IMapFrom<User>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}