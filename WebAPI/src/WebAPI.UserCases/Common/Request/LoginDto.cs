using System.ComponentModel.DataAnnotations;

namespace WebAPI.UserCases.Common.Request
{
    /// <summary>
    /// Login view model as contract form.
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