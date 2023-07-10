using _193089.Domain.DomainModels;
using _193089.Domain.DTO;
using _193089.Repository.Interface;
using _193089.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Service.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<TicketsInCart> _ticketInCartRepository;
        private readonly IUserRepository _userRepository;




        public TicketService(IRepository<Ticket> ticketRepository, IRepository<TicketsInCart> ticketInCartRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketInCartRepository = ticketInCartRepository;
            _userRepository = userRepository;
        }

        public bool AddToCart(AddToCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userCart = user.UserCart;

            if (item.TicketId != null && userCart != null)
            {
                var movieTicket = this.GetDetailsForTicket(item.TicketId);

                if (movieTicket != null)
                {
                    TicketsInCart ticketToAdd = new TicketsInCart
                    {
                        
                        Ticket = movieTicket,
                        TicketId = movieTicket.Id,
                        ShoppingCart = userCart,
                        CartId = userCart.Id,
                        Quantity = item.Quantity
                    };

                    this._ticketInCartRepository.Insert(ticketToAdd);
                    return true;
                }

                return false;
            }
            return false;
        }

        public void CreateNewTicket(Ticket t)
        {
            this._ticketRepository.Insert(t);
        }

        public void DeleteTicket(int id)
        {
            var ticket = this.GetDetailsForTicket(id);

            this._ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            return this._ticketRepository.GetAll().ToList();
        }

        public AddToCartDto GetCartInfo(int id)
        {
            var ticket = this.GetDetailsForTicket(id);
            AddToCartDto model = new AddToCartDto
            {
                SelectedTicket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };

            return model;
        }

        public Ticket GetDetailsForTicket(int id)
        {
            return this._ticketRepository.Get(id);
        }

        public void UpdateExistingTicket(Ticket t)
        {
            this._ticketRepository.Update(t);
        }
    }
}
