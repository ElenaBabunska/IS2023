using _193089.Domain.DomainModels;
using DocumentFormat.OpenXml.Office2010.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Controllers
{
    public class OrderController : Controller
    {
        public OrderController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44300/api/admin/GetAllActiveOrders";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Order>>().Result;
            return View();
        }
       
        public IActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44300/api/admin/GetOrderDetails";

            var model = new
            {
                Id = id,
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(URL, content).Result;
            var data = response.Content.ReadAsAsync<Order>().Result;

            return View(data);
        }

        public FileContentResult CreateInvoice(Guid? orderId)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44300/api/admin/GetOrderDetails";

            var model = new
            {
                Id = orderId
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}}", data.Id.ToString());
            document.Content.Replace("{{Email}}", data.User.Email);

            StringBuilder sb = new StringBuilder();
            double total = 0;
            foreach (var ticketInOrder in data.Tickets)
            {
                total += ticketInOrder.Quantity * ticketInOrder.OrderedTicket.MoviePrice;
                sb.AppendLine(ticketInOrder.OrderedTicket.MovieName + " with quantity of: " + ticketInOrder.Quantity + " and price of: " + ticketInOrder.OrderedTicket.MoviePrice + "MKD");
            }

            document.Content.Replace("{{TicketList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", total.ToString() + "MKD");

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());


            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");
        }
        


    }
}
