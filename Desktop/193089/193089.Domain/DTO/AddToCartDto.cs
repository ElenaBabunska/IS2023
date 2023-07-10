

using _193089.Domain.DomainModels;

namespace _193089.Domain.DTO
{
    public class AddToCartDto
    {
        public Ticket SelectedTicket { get; set; }
        public int TicketId { get; set; }
        public int Quantity { get; set; }
    }
}
