using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string RoleName { get; set; }
    }
}
