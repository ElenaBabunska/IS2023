
using _193089.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace _193089.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<TicketsInOrder> Tickets { get; set; }
    
        
    }
}
