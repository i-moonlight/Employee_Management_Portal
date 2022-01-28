using System.ComponentModel.DataAnnotations;

namespace WebAPI.UserCases.Common.Dto.Request
{
    /// <summary>
    /// The data transfer object for user login.
    /// </summary>
    public class LoginDto
    {
        [Required]
        [Display(Name = "Name")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}