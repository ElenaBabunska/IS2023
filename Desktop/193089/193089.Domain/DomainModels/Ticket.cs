using System.ComponentModel.DataAnnotations;

namespace _193089.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        

        [Required(ErrorMessage = "Name of the movie is required!")]
        public string MovieName { get; set; }

        [Required(ErrorMessage = "Image of the movie is required!")]
        public string MovieImage { get; set; }

        [Required(ErrorMessage = "Description of the movie is required!")]
        public string MovieDescription { get; set; }

        [Required(ErrorMessage = "Price of the ticket is required!")]
        public int MoviePrice { get; set; }

        [Required(ErrorMessage = "Rating of the movie is required!")]
        [Range(1, 5, ErrorMessage = "Rating of the movie starts from 1 to 5")]
        public int MovieRating { get; set; }

        [Required(ErrorMessage = "Category of the movie is required!")]
        public string MovieCategory { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Date of the movie is required!")]
        public DateTime Date { get; set; }

        public virtual List<TicketsInCart>? TicketsInCarts { get; set; }
        public virtual ICollection<TicketsInOrder> TicketInOrders { get; set; }
   
    }
}
