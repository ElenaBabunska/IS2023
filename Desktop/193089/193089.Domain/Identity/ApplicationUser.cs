

using _193089.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace _193089.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public virtual ShoppingCart UserCart { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public string Role { get; set; }

    }
}
