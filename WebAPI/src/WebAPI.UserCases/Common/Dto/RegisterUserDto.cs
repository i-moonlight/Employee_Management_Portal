using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Mappings;

namespace WebAPI.UserCases.Common.Dto
{
    /// <summary>
    /// Sets a properties of the data transfer object for user entity.
    /// </summary>
    public class RegisterUserDto : IMapFrom<User>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}