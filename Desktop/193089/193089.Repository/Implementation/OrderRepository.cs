using _193089.Domain.DomainModels;
using _193089.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }


        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.Tickets)
                .Include(z => z.User)
                .Include("TicketInOrders.OrderedTicket")
                .ToList();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
               .Include(z => z.Tickets)
               .Include(z => z.User)
               .Include("TicketInOrders.OrderedTicket")
               .SingleOrDefault(z => z.Id == model.Id);
        }
    }
}
