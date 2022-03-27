using System.ComponentModel.DataAnnotations;

namespace WebAPI.Service.Authentication.UseCases.Dto
{
    /// <summary>
    /// Sets a properties of the data transfer object for user login.
    /// </summary>
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}