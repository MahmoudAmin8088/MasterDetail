﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.Models
{
    public class RegisterModel
    {
        
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required, MaxLength(128)]
        public string Email { get; set; }
        [Required, MaxLength(256)]
        public string Password { get; set; }
        [Required, MaxLength(256)]
        public string ConfirmPassword { get; set; }
    }
}
