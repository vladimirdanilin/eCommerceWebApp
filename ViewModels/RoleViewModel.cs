using Microsoft.AspNetCore.Identity;

namespace ECommerceWebApp.ViewModels
{
    public class RoleViewModel
    {
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public List<IdentityRole<int>> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }

        public RoleViewModel()
        {
            AllRoles = new List<IdentityRole<int>>();
            UserRoles = new List<string>();
        }
    }
}
