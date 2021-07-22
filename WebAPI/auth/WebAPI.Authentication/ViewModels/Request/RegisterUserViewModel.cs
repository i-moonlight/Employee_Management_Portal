namespace WebAPI.Authentication.ViewModels.Request
{
    /// <summary>
    /// Register user view model.
    /// </summary>
    public class RegisterUserViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}