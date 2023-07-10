using System.ComponentModel.DataAnnotations.Schema;

namespace _193089.Domain.DomainModels
{
    public class TicketsInOrder : BaseEntity
    {
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket OrderedTicket { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order UserOrder { get; set; }

        public int Quantity { get; set; }
    }
}
