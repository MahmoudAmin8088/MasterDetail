using MasterDetails.Core.IRepository;
using MasterDetails.Core.IUnitOfWork;
using MasterDetails.Ef.Data;
using MasterDetails.Ef.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MasterDetails.Ef.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICustomerRepository Customers { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IitemRepository Items { get; private set; }

        public IOrderItemRepository OrderItems { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Customers = new  CustomerRepository(_context);
            Orders = new OrderRepository(_context);
            Items = new ItemRepository(_context);
            OrderItems = new OrderItemRepository(_context);

        }

        public int Complete()
        {
           return _context.SaveChanges(); 
        }

        public void Dispose()
        {
            _context.Dispose(); 

        }
    }
}
