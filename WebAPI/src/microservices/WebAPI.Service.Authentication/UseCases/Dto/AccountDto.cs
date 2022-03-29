using System.ComponentModel.DataAnnotations;

namespace WebAPI.Service.Authentication.UseCases.Dto
#nullable enable
{
    public class AccountDto
    {
        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        
        public string? ResetPasswordUri { get; set; }
    }
}