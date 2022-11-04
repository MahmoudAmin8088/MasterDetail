using MasterDetails.Core.IRepository;
using MasterDetails.Core.Models;
using MasterDetails.Ef.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Ef.Repository
{
    public class OrderItemRepository:BaseRepository<OrderItems>,IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepository(ApplicationDbContext context):base(context)
        {
            _context = context;

        }
    }
}
