﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EXAM.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Login { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password),ErrorMessage ="Passwords dont match")]
        public string ConfirmPassword { get; set; }
    }
}
