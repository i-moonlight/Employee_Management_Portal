using System.ComponentModel.DataAnnotations;

namespace WebAPI.UseCases.Common.Dto.Auth
{
    /// <summary>
    /// Sets a properties of the data transfer object for user login.
    /// </summary>
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        // [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}