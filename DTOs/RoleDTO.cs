using Microsoft.AspNetCore.Identity;

namespace ECommerceWebApp.DTOs
{
    public class RoleDTO
    {
        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public IList<string> UserRoles { get; set; }

        public List<IdentityRole<int>> AllRoles { get; set; }
    }
}
