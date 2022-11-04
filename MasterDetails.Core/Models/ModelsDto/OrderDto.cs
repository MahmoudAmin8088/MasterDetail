using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.Models.ModelsDto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        [Required]
        public string OrderNo { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string PMethod { get; set; }
        [Required]
        public decimal GTotal { get; set; }
        public IList<OrderItems> OrderItems { get; set; }

    }
}
