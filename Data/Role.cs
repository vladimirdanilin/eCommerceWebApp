using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebApp.Data
{
    [NotMapped]
    public class Role
    {
        public string Name { get; set; }

        public Role (string name) => Name = name;
    }
}
