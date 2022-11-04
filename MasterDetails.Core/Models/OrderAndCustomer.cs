using MasterDetails.Core.Models.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.Models
{
    public class OrderAndCustomer
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public string CustomerName { get; set; }
        public string PMethod { get; set; }
        public decimal GTotal { get; set; }



    }
}
