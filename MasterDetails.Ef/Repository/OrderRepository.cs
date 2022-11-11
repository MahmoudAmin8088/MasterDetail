using MasterDetails.Core.IRepository;
using MasterDetails.Core.Models;
using MasterDetails.Ef.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Ef.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context):base(context)
        {
            _context = context;

        }

        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);

            foreach (var item in order.OrderItems)
            {
                _context.OrderItems.Add(item);
            }
            return order;
        }

        public Order UpdateOrder(Order order)
        {
            _context.Orders.Update(order);

            foreach (var item in order.OrderItems)
            {
                _context.OrderItems.Update(item);
            }
            return order;
        }

       

        public ICollection<Order> GetCustomerOrders(int CustomerId)
        {
            var result = _context.Orders.Include(o=>o.Customer).Where(o=>o.CustomerId == CustomerId).ToList();
            return result;
        }

        public Object GetOrder(int ordeId)
        {
            var order = _context.Orders.Find(ordeId);

            var orderDetails = from a in _context.OrderItems
                               join i in _context.items
                               on a.ItemId equals i.ItemId
                               where a.OrderId == ordeId

                               select new
                               {
                                   a.OrderId,
                                   a.OrderItemId,
                                   a.ItemId,
                                   itemName = i.Name,
                                   i.Price,
                                   a.Quantity,
                                   total = a.Quantity * i.Price
                               };

            return new { order, orderDetails };
        }

        public List<Order> GetOrders()
        {
            var result = _context.Orders.Include(c =>c.Customer).ToList();
            return result;
        }

    }
}
