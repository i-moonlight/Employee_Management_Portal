using System.ComponentModel.DataAnnotations;

namespace WebAPI.Authentication.Models
{
    /// <summary>
    /// Login view model as contract form.
    /// </summary>
    public class LoginViewModel
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
