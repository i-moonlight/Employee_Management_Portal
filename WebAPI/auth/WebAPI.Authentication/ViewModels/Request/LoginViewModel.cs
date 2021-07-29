using System.ComponentModel.DataAnnotations;

namespace WebAPI.Authentication.ViewModels.Request
{
    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}