using System.ComponentModel.DataAnnotations.Schema;

namespace _193089.Domain.DomainModels
{
    public class TicketsInCart : BaseEntity
    {
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public ShoppingCart ShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}
