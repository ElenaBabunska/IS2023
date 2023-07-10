using _193089.Domain.DomainModels;
using _193089.Domain.Identity;
using _193089.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _193089.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITicketService _ticketService;

        public AdminController(IOrderService orderService, UserManager<ApplicationUser> userManager, ITicketService ticketService)
        {

            this._orderService = orderService;
            this._ticketService = ticketService;
            this.userManager = userManager;
        }
        //TICKET API ACTIONS
        [HttpGet("[action]")]
        public List<Ticket> GetAllTickets()
        {
            return this._ticketService.GetAllTickets();
        }

        [HttpGet("[action]")]
        public List<Ticket> GetAllTicketsByCategory(string categoryName)
        {
            return this._ticketService.GetAllTickets().Where(z => z.MovieCategory.Equals(categoryName)).ToList();
        }

        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return this._orderService.GetAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return this._orderService.GetOrderDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<ApplicationUser> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var userCheck = userManager.FindByEmailAsync(item.Email).Result;

                if (userCheck == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role = item.Role,
                        UserCart = new ShoppingCart()
                    };
                    var result = userManager.CreateAsync((ApplicationUser)user).Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, user.Role);
                    }

                    status = status && result.Succeeded;


                }
                else
                {
                    continue;
                }
            }

            return status;
        }
    }
}
