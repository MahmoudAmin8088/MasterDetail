using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required,MaxLength(200)]
        public string CustomerName { get; set; }
    }
}
