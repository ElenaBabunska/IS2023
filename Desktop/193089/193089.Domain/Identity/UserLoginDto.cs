using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Domain.Identity
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Your email is required!")]
        [EmailAddress(ErrorMessage = "You've entered an invalid email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me!")]
        public bool RememberMe { get; set; }
    }
}
