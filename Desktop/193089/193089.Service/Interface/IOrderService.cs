using _193089.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();

        Order GetOrderDetails(BaseEntity model);
    }
}
