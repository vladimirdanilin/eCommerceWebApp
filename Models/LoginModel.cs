using System.ComponentModel.DataAnnotations;

namespace eCommerceWebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
