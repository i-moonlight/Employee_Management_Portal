using System.ComponentModel.DataAnnotations;

#nullable enable
namespace WebAPI.UseCases.Common.Dto.Auth
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}