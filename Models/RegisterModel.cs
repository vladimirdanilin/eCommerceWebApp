using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
