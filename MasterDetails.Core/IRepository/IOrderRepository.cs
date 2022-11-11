using MasterDetails.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.IRepository
{
    public interface IOrderRepository:IBaseRepository<Order>
    {
        ICollection<Order> GetCustomerOrders(int CustomerId);
        Order CreateOrder(Order order);
        Order UpdateOrder(Order order);
        List<Order> GetOrders();
         Object GetOrder(int id);
    }
}
