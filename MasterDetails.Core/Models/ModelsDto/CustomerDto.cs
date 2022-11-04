using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.Models.ModelsDto
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
    }
}
