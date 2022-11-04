using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MasterDetails.Core.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required, MaxLength(100)]
        public string? FullName { get; set; }


    }
}
