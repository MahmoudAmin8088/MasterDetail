using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required,MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual IList<OrderItems> OrderItems { get; set; }
    }
}
