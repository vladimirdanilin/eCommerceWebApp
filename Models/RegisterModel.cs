using System.ComponentModel.DataAnnotations;

namespace eCommerceWebApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Full name is not specified")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
