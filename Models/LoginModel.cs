using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ValidateNever]
        public string? ReturnURL { get; set; }
    }
}
