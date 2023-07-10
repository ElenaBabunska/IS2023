using _193089.Domain.DomainModels;

namespace _193089.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketsInCart> ticketsInCarts { get; set; }

        public int TotalPrice { get; set; }
    }
}
