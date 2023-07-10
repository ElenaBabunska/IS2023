using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Domain
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "Category of the movie is required!")]

        public string Category { get; set; }
    }
}
