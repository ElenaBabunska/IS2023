
using _193089.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace _193089.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        
        public string OwnerId { get; set; }
        public ApplicationUser CartOwner { get; set; }
        public virtual ICollection<TicketsInCart>? TicketsInCarts { get; set; }
    }
}
