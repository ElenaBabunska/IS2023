﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _193089.Domain
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Name of the role is required!")]

        public string RoleName { get; set; }
    }
}
