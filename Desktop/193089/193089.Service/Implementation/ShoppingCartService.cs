using _193089.Domain.DomainModels;
using _193089.Domain.DTO;
using _193089.Repository.Interface;
using _193089.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {

        public readonly IRepository<ShoppingCart> _cartRepository;
        public readonly IUserRepository _userRepository;
        public readonly IRepository<Order> _orderRepository;
        public readonly IRepository<TicketsInOrder> _ticketInOrderRepository;
        public readonly IRepository<EmailMessage> _emailMessageRepository;


        public ShoppingCartService(IRepository<ShoppingCart> cartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<TicketsInOrder> ticketInOrderRepository, IRepository<EmailMessage> emailMessageRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
            _emailMessageRepository = emailMessageRepository;
        }

        public bool deleteTicketFromCart(string userId, int id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var cart = loggedInUser.UserCart;

                var ticketToDelete = cart.TicketsInCarts
                    .Where(z => z.TicketId == id)
                    .FirstOrDefault();

                cart.TicketsInCarts.Remove(ticketToDelete);

                this._cartRepository.Update(cart);

                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var cart = loggedInUser.UserCart;
            
            var allTickets = cart.TicketsInCarts.ToList();


            var allTicketsPrice = allTickets.Select(z => new
            {
                price = z.Ticket.MoviePrice,
                quantity = z.Quantity
            }).ToList();

            int total = 0;

            foreach (var item in allTicketsPrice)
            {
                total += item.price * item.quantity;
            }


            ShoppingCartDto cartDtoItem = new ShoppingCartDto
            {
                ticketsInCarts = allTickets,
                TotalPrice = total
            };

            return cartDtoItem;
        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {

                var loggedInUser = this._userRepository.Get(userId);

                var cart = loggedInUser.UserCart;

                EmailMessage message = new EmailMessage()
                {
                    MailTo = loggedInUser.Email,
                    Subject = "Successfully created order",
                    Status = false
                };


                Order orderItem = new Order
                {
                   
                    UserId = userId,
                    User = loggedInUser
                };

                this._orderRepository.Insert(orderItem);

                
                List<TicketsInOrder> ticketInOrders = new List<TicketsInOrder>();

                var result = cart.TicketsInCarts.Select(z => new TicketsInOrder
                {
                    
                    OrderId = orderItem.Id,
                    TicketId = z.Ticket.Id,
                    OrderedTicket = z.Ticket,
                    Quantity = z.Quantity,
                    UserOrder = orderItem

                }).ToList();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Your order is completed. The order contains: ");
                
                int totalPrice = 0;
                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    totalPrice += item.Quantity * item.OrderedTicket.MoviePrice;
                    sb.AppendLine(i.ToString() + ". " + item.OrderedTicket.MovieName + " with a price of: " + item.OrderedTicket.MoviePrice + "MKD and a quantity of: " + item.Quantity);
                }
                sb.AppendLine("Your Total Price: " + totalPrice.ToString());

                message.Content = sb.ToString();

                

                foreach (var item in ticketInOrders)
                {
                    this._ticketInOrderRepository.Insert(item);
                }
                

                loggedInUser.UserCart.TicketsInCarts.Clear();

                this._emailMessageRepository.Insert(message);

                this._userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}
