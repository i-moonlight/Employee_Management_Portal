using System.ComponentModel.DataAnnotations;

namespace WebAPI.Authentication.Models
{
    public class RegistrationEmployeeModel
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string Email { get; set; }
    }
}