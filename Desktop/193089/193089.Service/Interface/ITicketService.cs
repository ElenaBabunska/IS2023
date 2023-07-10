using _193089.Domain.DomainModels;
using _193089.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Service.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();

        Ticket GetDetailsForTicket(int id);

        void CreateNewTicket(Ticket t);

        void UpdateExistingTicket(Ticket t);

        AddToCartDto GetCartInfo(int id);

        void DeleteTicket(int id);

        bool AddToCart(AddToCartDto item, string userID);
    }
}
