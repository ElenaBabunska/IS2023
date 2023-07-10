using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Domain.Identity
{
    public class UserRegistrationDto
    {

        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "You've entered an invalid email!")]
        [Required(ErrorMessage = "Your email is required!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Your password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Your confirmed password is required!")]
        [Compare("Password", ErrorMessage = "Your passwords do not match!")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
