using System.ComponentModel.DataAnnotations;

namespace Hangfire.Models.ViewModels
{
    public class UserLoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set;}

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
