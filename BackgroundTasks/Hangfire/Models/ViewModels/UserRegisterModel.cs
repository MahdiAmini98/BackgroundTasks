using System.ComponentModel.DataAnnotations;

namespace Hangfire.Models.ViewModels
{
    public class UserRegisterModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "PhoneNumber is Required")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is Required")]
        [DataType(dataType:DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Password is Required")]
        [DataType(dataType: DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password and confirmation Password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
